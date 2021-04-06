$(window).load(() => {
    InitPrivacyModalButonEvents();
});

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
        Id: g_editPrivacyElementId,
        PrivacyLevel: privacyLevel,
        BeginDate: beginDate,
        EndDate: endDate,
        IsAlways: isAlways
    };

    switch (g_currentAddButtonActionType) {
        case AddButtonActionTypes.AddDataHolder: {
            if (!UpdateDataHolderPrivacy(privacy)) {
                alert("Ошибка при изменении приватности ячейки данных.");
                return;
            }
            RefreshDataHolders();
            break;
        }

        case AddButtonActionTypes.AddImage: {
            if (!UpdateImagePrivacy(privacy)) {
                alert("Ошибка при изменении приватности изображения.");
                return;
            }
            break;
        }

        case AddButtonActionTypes.AddVideo: {
            if (!UpdateVideoPrivacy(privacy)) {
                alert("Ошибка при изменении приватности видео.");
                return;
            }
            break;
        }

        default:
            break;
    }

    editPrivacyModal.modal("hide");
}

// Requests
//TODO:
function GetImagePrivacy(imageId) {

}

//TODO:
function GetVideoPrivacy(videoId) {

}

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

        if (dataHolderPrivacy.IsAlways) {
            privacyModal
                .find("input[name=\"limit-type\"][value=\"0\"]")
                .parent()
                .click();
        }
        else {
            privacyModal
                .find("input[name=\"limit-type\"][value=\"1\"]")
                .parent()
                .click();            
        }      

        let beginDate = dataHolderPrivacy.BeginDate.substr(0, dataHolderPrivacy.BeginDate.lastIndexOf(":"));
        let endDate = dataHolderPrivacy.EndDate.substr(0, dataHolderPrivacy.EndDate.lastIndexOf(":"));

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

function UpdateDataHolderPrivacy(dataHolderPrivacy) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: dataHolderPrivacy,
        url: "/Privacy/UpdateDataHolderPrivacy/" + dataHolderPrivacy.Id,
        success: function (response) {
            result = true;
        }
    });

    return result;
}

//TODO:
function UpdateImagePrivacy(imagePrivacy) {

}

//TODO:
function UpdateVideoPrivacy(videoPrivacy) {

}