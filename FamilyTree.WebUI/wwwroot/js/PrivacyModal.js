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

            let beginDate = new Date(dataHolderPrivacy.BeginDate);
            let endDate = new Date(dataHolderPrivacy.EndDate);

            privacyModal
                .find("#privacy-level-begin-date")
                .val(beginDate.toISOString());

            privacyModal
                .find("#privacy-level-end-date")
                .val(endDate.toISOString());
        }                
    }
}

//TODO:
function LoadImagePrivacyData(imageId) {

}

//TODO:
function LoadVideoPrivacyData(videoId) {

}

function UpdateDataHolderPrivacy(dataHolderPrivacy) {

}

//TODO:
function UpdateImagePrivacy(imagePrivacy) {

}

//TODO:
function UpdateVideoPrivacy(videoPrivacy) {

}