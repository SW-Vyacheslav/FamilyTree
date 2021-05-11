$(window).load(() => {
    InitPrivacyModalButonEvents();
    InitPrivacyNotifications();
});

let g_privacyNotificationsConnection = null;

//Notifications
function InitPrivacyNotifications() {
    g_privacyNotificationsConnection = new signalR.HubConnectionBuilder()
        .withUrl("/Privacy/Notifications")
        .build();

    g_privacyNotificationsConnection.on("ReceivePrivacyChangedNotification", (privacyId) => {
        if (g_currentAddButtonActionType == AddButtonActionTypes.AddDataHolder) {
            let dataHolderIndex = g_currentDataBlock.DataHolders
                .findIndex((item) => item.Privacy.Id == privacyId);

            if (dataHolderIndex !== -1) {
                let dataHolderId = g_currentDataBlock.DataHolders[dataHolderIndex].Id;

                GetDataHolder(dataHolderId).then((result) => {
                    g_currentDataBlock.DataHolders[dataHolderIndex] = result;
                    UpdateDataHolders();
                }, (r) => console.error(r));
            }
        }
    });

    g_privacyNotificationsConnection.start()
        .catch(error => console.error(error.Message));
}

// Requests
async function UpdatePrivacy(privacy) {
    let result = await $.ajax({
        type: "PUT",
        data: privacy,
        url: "/Privacy/Update/" + privacy.Id
    });

    return result;
}

//Events
function InitPrivacyModalButonEvents() {
    $("#privacy-level-modal")
        .find("input[name=\"privacy-level\"]")
        .parent()
        .click(OnPrivacyLevelButtonClick);

    $("#privacy-level-modal")
        .find("input[name=\"limit-type\"]")
        .parent()
        .click(OnLimitTypeButtonClick);
    $("#privacy-level-modal")
        .find("#edit-privacy-level-submit-button")
        .click(OnEditPrivacyLevelSubmitButtonClick);
}

function OnPrivacyLevelButtonClick(event) {
    let privacyLevelValue = $(event.currentTarget)
        .find("input")
        .val();

    if (privacyLevelValue == PrivacyLevels.InternalUse) {
        $("#privacy-level-modal")
            .find("#privacy-level-accounts")
            .css("display", "block");
    }
    else {
        $("#privacy-level-modal")
            .find("#privacy-level-accounts")
            .css("display", "none");
    }
}

function OnLimitTypeButtonClick(event) {
    let limitTypeValue = $(event.currentTarget)
        .find("input")
        .val();

    if (limitTypeValue == 0) {
        $("#privacy-level-modal")
            .find("#privacy-level-limits")
            .css("display", "none");
    }
    else {
        $("#privacy-level-modal")
            .find("#privacy-level-limits")
            .css("display", "block");
    }
}

function OnEditPrivacyLevelSubmitButtonClick(event) {
    let editPrivacyModal = $("#privacy-level-modal");
    let beginDate = editPrivacyModal.find("#privacy-level-begin-date").val();
    let endDate = editPrivacyModal.find("#privacy-level-end-date").val();
    let isAlways = editPrivacyModal.find("input[name=\"limit-type\"]:checked").val() == "0";
    let privacyLevel = editPrivacyModal.find("input[name=\"privacy-level\"]:checked").val();

    let privacy = {
        Id: 0,
        PrivacyLevel: privacyLevel,
        BeginDate: beginDate,
        EndDate: endDate,
        IsAlways: isAlways
    };

    switch (g_currentAddButtonActionType) {
        case AddButtonActionTypes.AddDataHolder: {
            let dataHolder = g_currentDataBlock.DataHolders
                .find((item) => item.Id == g_editPrivacyElementId);

            privacy.Id = dataHolder.Privacy.Id;

            UpdatePrivacy(privacy).then((result) => {
                RefreshDataHolders();
                UpdateDataHolders();
                editPrivacyModal.modal("hide");
            }, (r) => {
                alert("Ошибка при изменении приватности ячейки данных.");
            });
            
            break;
        }

        case AddButtonActionTypes.AddImage: {
            let image = g_currentDataBlockImages
                .find((item) => item.Id == g_editPrivacyElementId);

            privacy.Id = image.Privacy.Id;

            UpdatePrivacy(privacy).then((result) => {
                RefreshImages();
                editPrivacyModal.modal("hide");
            }, (r) => {
                alert("Ошибка при изменении приватности изображения.");
            });
            
            break;
        }

        case AddButtonActionTypes.AddVideo: {
            let video = g_currentDataBlockVideos
                .find((item) => item.Id == g_editPrivacyElementId);

            privacy.Id = video.Privacy.Id;

            UpdatePrivacy(privacy).then((result) => {
                RefreshVideos();
                editPrivacyModal.modal("hide");
            }, (r) => {
                alert("Ошибка при изменении приватности видео.");
            });
            
            break;
        }

        default:
            break;
    }
}

//UI
function LoadDataHolderPrivacyData(dataHolderId) {
    let dataHolderPrivacy = g_currentDataBlock
        .DataHolders
        .find(dh => dh.Id == dataHolderId)
        .Privacy;

    let privacyModal = $("#privacy-level-modal");

    if (dataHolderPrivacy == null) {
        privacyModal
            .find(".privacy-confidential")
            .click();

        privacyModal
            .find("input[name=\"limit-type\"][value=\"0\"]")
            .parent()
            .click();
    }
    else {
        privacyModal
            .find("input[name=\"privacy-level\"][value=\"" + dataHolderPrivacy.PrivacyLevel + "\"]")
            .parent()
            .click();

        privacyModal
            .find("input[name=\"privacy-level\"][value=\"" + dataHolderPrivacy.PrivacyLevel + "\"]")
            .prop("checked", true);

        if (dataHolderPrivacy.IsAlways) {
            privacyModal
                .find("input[name=\"limit-type\"][value=\"0\"]")
                .parent()
                .click();

            privacyModal
                .find("input[name=\"limit-type\"][value=\"0\"]")
                .prop("checked", true);
        }
        else {
            privacyModal
                .find("input[name=\"limit-type\"][value=\"1\"]")
                .parent()
                .click();

            privacyModal
                .find("input[name=\"limit-type\"][value=\"1\"]")
                .prop("checked", true);
        }

        let beginDate = UTCDateToLocaleString(new Date(dataHolderPrivacy.BeginDate.replace("T", " ") + " UTC"));               
        let endDate = UTCDateToLocaleString(new Date(dataHolderPrivacy.EndDate.replace("T", " ") + " UTC"));

        privacyModal
            .find("#privacy-level-begin-date")
            .val(beginDate);

        privacyModal
            .find("#privacy-level-end-date")
            .val(endDate);
    }
}

//TODO:
function LoadImagePrivacyData(imageId) {

}

//TODO:
function LoadVideoPrivacyData(videoId) {

}

function UTCDateToLocaleString(date) {
    var newDate = new Date(
        Date.UTC(date.getFullYear(),
            date.getMonth(),
            date.getDate(),
            date.getHours(),
            date.getMinutes(),
            date.getSeconds()));

    let newDateISOStr = newDate.toISOString();

    return newDateISOStr.substring(0, newDateISOStr.lastIndexOf(":"));
}