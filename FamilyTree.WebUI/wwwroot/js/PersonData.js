﻿$(window).load(() => {
    InitPersonDataBlockButtonEvents();
});

const DataCategoryTypes = {
    InfoBlock: 0,
    ListBlock: 1,
    PersonInfo: 2,
    Education: 3,
    Residencies: 4,
    LaborActivities: 5,
    ImportantEvents: 6   
};
const DataHolderTypes = {
    Text: 0,
    TextArea: 1,
    Date: 2,
    Time: 3,
    DateTime: 4,
    Name: 5,
    Surname: 6,
    MiddleName: 7,
    Birthday: 8,
    Gender: 9
};
const DataBlockContentTabs = {
    Data: 0,
    Images: 1,
    Videos: 2
};
const GenderTypes = {
    Unknown: "Unknown",
    Male: "Male",
    Female: "Female"
}
const AddButtonActionTypes = {
    AddDataBlock: 0,
    AddDataHolder: 1,
    AddImage: 2,
    AddVideo: 3
};
const PrivacyLevels = {
    TopSecret: 0,
    Confidential: 1,
    InternalUse: 2,
    PublicUse: 3
};
const CopyObjectTypes = {
    DataCategory: 0,
    DataBlock: 1,
    DataHolder: 2,
    Image: 3,
    Video: 4
};

const WaitForMilliseconds = (ms) => new Promise(handler => setTimeout(handler, ms));

let g_currentPersonId = null;
let g_currentDataCategory = null;
let g_currentDataBlock = null;
let g_currentDataBlockImages = null;
let g_currentDataBlockVideos = null;
let g_currentAddButtonActionType = null;
let g_editElementId = null;
let g_editPrivacyElementId = null;
let g_isSaving = false;
let g_copyObject = null;
let g_isUploadingVideo = false;

function LoadPersonData(personId) {
    let dataCategories = GetDataCategories(personId);

    if (dataCategories.length === 0)
        return false;

    g_currentPersonId = personId;

    ClearDataCategories();

    dataCategories.forEach((item) => {
        AddItemToDataCategories(item);
    });

    new Sortable($(".person-data-block__data-categories")[0], {
        handle: ".data-categories__item",
        animation: 500,
        onEnd: (event) => {
            let dataCategory = {
                id: $(event.item).attr("data-id"),
                command: {
                    Id: $(event.item).attr("data-id"),
                    Order: event.newIndex + 1
                }
            };

            UpdateDataCategoryOrder(dataCategory);
        }
    });

    $("#person-data-block")
        .find(".data-categories")
        .find(".data-categories__item")
        .click(OnDataCategoryClick);

    $("#person-data-block")
        .find(".data-categories")
        .find(".data-categories__item")[0]
        .click();

    return true;
}

//Requests
function GetDataCategories(personId) {
    let result = [];
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/PersonContent/GetDataCategories?personId=" + personId,
        success: function (data) {
            result = data;
        }
    });
    return result;
}

function GetDataCategory(dataCategoryId) {
    let result = {};
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/PersonContent/GetDataCategory?dataCategoryId=" + dataCategoryId,
        success: function (data) {
            result = data;
        }
    });
    return result;
}

async function GetImages(dataBlockId) {
    const result = await $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Media/GetImages?dataBlockId=" + dataBlockId
    });

    return result;
}

async function GetVideos(dataBlockId) {
    const result = await $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Media/GetVideos?dataBlockId=" + dataBlockId
    });

    return result;
}

/**
 * Send request to create DataCategory. Returns created DataCategory object Id or -1, if not created.
 * @param {object} dataCategory Object { CategoryType: string, Name: string, PersonId: number }
 * @returns {number}
 */
function CreateDataCategory(dataCategory) {
    let result = -1;

    $.ajax({
        async: false,
        type: "POST",
        data: dataCategory,
        url: "/PersonContent/CreateDataCategory",
        success: function (response) {
            result = response;
        }
    });

    return result;
}

function CreateDataBlock(dataBlock) {
    let result = -1;

    $.ajax({
        async: false,
        type: "POST",
        data: dataBlock,
        url: "/PersonContent/CreateDataBlock",
        success: function (response) {
            result = response;
        }
    });

    return result;
}

function CreateDataHolder(dataHolder) {
    let result = -1;

    $.ajax({
        async: false,
        type: "POST",
        data: dataHolder,
        url: "/PersonContent/CreateDataHolder",
        success: function (response) {
            result = response;
        }
    });

    return result;
}

function CreateImage(image) {
    let result = -1;

    $.ajax({
        async: false,
        type: "POST",
        data: image,
        contentType: false,
        processData: false,
        url: "/Media/CreateImage",
        success: function (response) {
            result = response;
        }
    });

    return result;
}

async function CreateVideo(video) {
    const result = await $.ajax({
        type: "POST",
        data: video,
        cache: false,
        contentType: false,
        processData: false,
        url: "/Media/CreateVideo",
        xhr: function () {
            var myXhr = $.ajaxSettings.xhr();
            if (myXhr.upload) {
                myXhr.upload.addEventListener("progress", function (e) {
                    if (e.lengthComputable) {
                        $("#add-video-modal #video-upload-progress")
                            .attr({
                                value: e.loaded,
                                max: e.total
                            });
                    }
                }, false);
            }
            return myXhr;
        }
    });

    return result;
}

function UpdateDataCategoryName(dataCategory) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: dataCategory,
        url: "/PersonContent/UpdateDataCategoryName",
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function UpdateDataCategoryOrder(dataCategory) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: dataCategory,
        url: "/PersonContent/UpdateDataCategoryOrder",
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function UpdateDataBlockTitle(dataBlock) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: dataBlock,
        url: "/PersonContent/UpdateDataBlockTitle",
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function UpdateDataBlockOrder(dataBlock) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: dataBlock,
        url: "/PersonContent/UpdateDataBlockOrder",
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function UpdateDataHolderTitle(dataHolder) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: dataHolder,
        url: "/PersonContent/UpdateDataHolderTitle",
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function UpdateDataHolderData(dataHolder) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: dataHolder,
        url: "/PersonContent/UpdateDataHolderData",
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function UpdateDataHolderOrder(dataHolder) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: dataHolder,
        url: "/PersonContent/UpdateDataHolderOrder",
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function UpdateImageDetails(image) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: image,
        url: "/Media/UpdateImageDetails/" + image.Id,
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function UpdateVideoDetails(video) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: video,
        url: "/Media/UpdateVideoDetails/" + video.Id,
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function UpdatePersonAvatarImage(personId, imageId) {
    let result = false;

    $.ajax({
        async: false,
        type: "PUT",
        data: {
            Id: personId,
            ImageId: imageId
        },
        url: "/People/UpdatePersonAvatarImage/" + personId,
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function SaveDataHolders() {
    let isSaved = [];

    let dataHolders = $("#person-data-block")
        .find(".data-holders .data-holders__item");

    dataHolders.each((i, el) => {
        let data = "";

        if (el.classList.contains("data-holder-gender")) {
            data = $(el).find("input:checked").val();
        }
        else if (el.classList.contains("data-holder-textarea")) {
            data = $(el).find("textarea").val();
        }
        else {
            data = $(el).find(".data-holder__data input").val();
        }

        let dataHolder = {
            id: el.getAttribute("data-id"),
            command: {
                Id: el.getAttribute("data-id"),
                Data: data
            }
        };

        isSaved.push(UpdateDataHolderData(dataHolder));
    });

    return isSaved.find(el => el == false) == null;
}

function CopyDataBlocks(ids, dataCategoryId) {
    let result = false;

    $.ajax({
        async: false,
        type: "POST",
        data: {
            DataBlocksIds: ids,
            DataCategoryId: dataCategoryId
        },
        url: "/PersonContent/CopyDataBlocks",
        success: function (response) {
            result = true;
        }
    });

    return result;
}

function CopyDataHolders(ids, dataBlockId) {
    let result = false;

    $.ajax({
        async: false,
        type: "POST",
        data: {
            DataHoldersIds: ids,
            DataBlockId: dataBlockId
        },
        url: "/PersonContent/CopyDataHolders",
        success: function (response) {
            result = true;
        }
    });

    return result;
}

async function CopyImages(ids, dataBlockId) {
    const  result = await $.ajax({
        type: "POST",
        data: {
            ImagesIds: ids,
            DataBlockId: dataBlockId
        },
        url: "/Media/CopyImages"
    });

    return result;
}

async function CopyVideos(ids, dataBlockId) {
    const result = await $.ajax({
        type: "POST",
        data: {
            VideosIds: ids,
            DataBlockId: dataBlockId
        },
        url: "/Media/CopyVideos"
    });

    return result;
}

async function DeleteDataBlock(dataBlockId) {
    const result = await $.ajax({
        type: "DELETE",
        url: "/PersonContent/DeleteDataBlock/" + dataBlockId
    });

    return result;
}

async function DeleteDataHolder(dataHolderId) {
    const result = await $.ajax({
        type: "DELETE",
        url: "/PersonContent/DeleteDataHolder/" + dataHolderId
    });

    return result;
}

async function DeleteImage(imageId) {
    const result = await $.ajax({
        type: "DELETE",
        url: "/Media/DeleteImage/" + imageId
    });

    return result;
}

async function DeleteVideo(videoId) {
    let result = await $.ajax({
        type: "DELETE",
        url: "/Media/DeleteVideo/" + videoId
    });

    return result;
}

//Events
function InitPersonDataBlockButtonEvents() {
    $("#person-data-block")
        .find(".data-block-content")
        .find(".tabs-buttons")
        .find(".tabs-buttons__button")
        .click(OnTabButtonClick);

    $("#add-data-category-button")
        .click(OnAddDataCategoryButtonClick);

    $("#add-data-category-modal")
        .find("#add-data-category-submit-button")
        .click(OnAddDataCategorySubmitButtonClick);

    $("#add-data-block-modal")
        .find("#add-data-block-submit-button")
        .click(OnAddDataBlockSubmitButtonClick);

    $("#add-data-holder-modal")
        .find("#add-data-holder-submit-button")
        .click(OnAddDataHolderSubmitButtonClick);

    $("#back-to-data-blocks-button")
        .click(OnBackToDataBlocksButtonClick);

    $("#add-element-button")
        .click(OnAddElementButtonClick);

    $("#delete-button")
        .click(OnDeleteButtonClick);

    $("#edit-element-button")
        .click(OnEditElementButtonClick);

    $("#copy-button")
        .click(OnCopyButtonClick);

    $("#paste-button")
        .click(OnPasteButtonClick);

    $("#person-data-block")
        .find("#save-button")
        .click(OnSaveButtonClick);

    $("#edit-privacy-button")
        .click(OnEditPrivacyButtonClick);

    $("#edit-data-category-modal")
        .find("#edit-data-category-submit-button")
        .click(OnEditDataCategorySubmitButtonClick);

    $("#edit-data-block-modal")
        .find("#edit-data-block-submit-button")
        .click(OnEditDataBlockSubmitButtonClick);

    $("#edit-data-holder-modal")
        .find("#edit-data-holder-submit-button")
        .click(OnEditDataHolderSubmitButtonClick);

    $("#add-image-modal")
        .find("#add-image-submit-button")
        .click(OnAddImageSubmitButtonClick);

    $("#image-carousel-modal")
        .find("#save-image-submit-button")
        .click(OnSaveImageSubmitButtonClick);

    $("#image-carousel-modal")
        .find("#set-image-as-avatar-button")
        .click(OnSetImageAsAvatarButtonClick);

    $("#add-video-modal")
        .find("#add-video-submit-button")
        .click(OnAddVideoSubmitButtonClick);

    $("#video-modal")
        .find("#save-video-submit-button")
        .click(OnSaveVideoSubmitButtonClick);

    $("#delete-modal")
        .find("#delete-submit-button")
        .click(OnDeleteSubmitButtonClick);
}

function OnBackToDataBlocksButtonClick() {
    ShowDataBlocks();
    ShowDataBlockContent(false);
    ShowBackToDataBlocksButton(false);
    ShowSaveButton(false);
    g_currentAddButtonActionType = AddButtonActionTypes.AddDataBlock;
}

function OnTabButtonClick(event) {
    let targetElement = $(event.currentTarget);

    let dataBlockContentTabsButtonsElement = $("#person-data-block")
        .find(".data-block-content")
        .find(".tabs-buttons");

    if (targetElement.hasClass("tab-button-data")) {
        ShowDataBlockContentTab(DataBlockContentTabs.Data);
        g_currentAddButtonActionType = AddButtonActionTypes.AddDataHolder;
        ShowSaveButton();
    }
    else if (targetElement.hasClass("tab-button-images")) {
        ShowDataBlockContentTab(DataBlockContentTabs.Images);
        g_currentAddButtonActionType = AddButtonActionTypes.AddImage;
        ShowSaveButton(false);
    }
    else if (targetElement.hasClass("tab-button-videos")) {
        ShowDataBlockContentTab(DataBlockContentTabs.Videos);
        g_currentAddButtonActionType = AddButtonActionTypes.AddVideo;
        ShowSaveButton(false);
    }
    else {
        return;
    }

    dataBlockContentTabsButtonsElement
        .children()
        .removeClass("tabs-buttons__button_active");

    targetElement.addClass("tabs-buttons__button_active");
}

function OnDataCategoryClick(event) {
    let dataCategoryId = $(event.currentTarget).attr("data-id");
    g_currentDataCategory = GetDataCategory(dataCategoryId);

    if (g_currentDataCategory == null)
        return;

    if (g_currentDataCategory.DataCategoryType == DataCategoryTypes.InfoBlock ||
        g_currentDataCategory.DataCategoryType == DataCategoryTypes.PersonInfo) {
        ShowDataBlocks(false);
        ShowDataBlockContent();

        g_currentDataBlock = g_currentDataCategory.DataBlocks[0];

        UpdateDataHolders();
        RefreshImages().then((val) => UpdateImages());
        RefreshVideos().then((val) => UpdateVideos());

        OpenDefaultDataBlockTab();
    }
    else if (g_currentDataCategory.DataCategoryType == DataCategoryTypes.ListBlock ||
        g_currentDataCategory.DataCategoryType == DataCategoryTypes.Education ||
        g_currentDataCategory.DataCategoryType == DataCategoryTypes.ImportantEvents ||
        g_currentDataCategory.DataCategoryType == DataCategoryTypes.LaborActivities ||
        g_currentDataCategory.DataCategoryType == DataCategoryTypes.Residencies) {

        ShowDataBlocks();
        ShowDataBlockContent(false);
        ShowSaveButton(false);

        g_currentAddButtonActionType = AddButtonActionTypes.AddDataBlock;
                
        UpdateDataBlocks();
    }
    else {
        return;
    }

    ShowBackToDataBlocksButton(false);

    $("#person-data-block")
        .find(".data-categories")
        .find(".data-categories__item")
        .removeClass("data-categories__item_active");

    $(event.currentTarget)
        .addClass("data-categories__item_active");
}

function OnDataBlockClick(event) {
    if ($(event.target).is("input")) return;

    let dataBlockId = $(event.currentTarget).attr("data-id");
    let dataBlock = g_currentDataCategory.DataBlocks
        .find((item) => item.Id == dataBlockId);

    g_currentDataBlock = dataBlock;
    g_currentAddButtonActionType = AddButtonActionTypes.AddDataHolder;

    UpdateDataHolders();
    RefreshImages().then((val) => UpdateImages());
    RefreshVideos().then((val) => UpdateVideos());

    ShowBackToDataBlocksButton();
    ShowDataBlocks(false);
    ShowDataBlockContent();
    OpenDefaultDataBlockTab();
}

function OnImageClick(event) {
    if ($(event.target).is("input")) return;

    let imageId = $(event.currentTarget).attr("data-id");

    UpdateImageSlider(imageId);

    $("#image-carousel-modal").modal("show");
}

function OnVideoClick(event) {
    if ($(event.target).is("input")) return;

    let videoId = $(event.currentTarget).attr("data-id");

    UpdateVideoModalVideos();

    $("#video-modal .videos-list .videos-list__item[data-id=\"" + videoId + "\"]")
        .click();

    $("#video-modal").modal("show");
}

function OnAddDataCategoryButtonClick() {
    $('#add-data-category-modal').modal("show");
}

function OnAddDataCategorySubmitButtonClick() {
    let dataCategory = {
        DataCategoryType: $("#add-data-category-modal").find("#data-category-type").val(),
        Name: $("#add-data-category-modal").find("#data-category-name").val(),
        PersonId: g_currentPersonId
    };

    if (CreateDataCategory(dataCategory) === -1) {
        alert("Ошибка при создании категории данных.");
    }
    else {
        $("#add-data-category-modal").modal("hide");
        LoadPersonData(g_currentPersonId);
    }
}

function OnAddElementButtonClick() {
    switch (g_currentAddButtonActionType) {
        case AddButtonActionTypes.AddDataBlock: {
            $("#add-data-block-modal").modal("show");
            break;
        }
        case AddButtonActionTypes.AddDataHolder: {
            $("#add-data-holder-modal").modal("show");
            break;
        }
        case AddButtonActionTypes.AddImage: {
            $("#add-image-modal").modal("show");
            break;
        }
        case AddButtonActionTypes.AddVideo: {
            $("#add-video-modal").modal("show");
            break;
        }

        default:
            break;
    }
}

function OnAddDataBlockSubmitButtonClick() {
    let dataBlock = {
        Title: $("#add-data-block-modal").find("#data-block-title").val(),
        DataCategoryId: g_currentDataCategory.Id
    };

    if (CreateDataBlock(dataBlock) === -1) {
        alert("Ошибка при создании блока данных.");
    }
    else {
        $("#add-data-block-modal").modal("hide");
        RefreshDataBlocks();
    }
}

function OnAddDataHolderSubmitButtonClick() {
    let dataHolder = {
        DataHolderType: $("#add-data-holder-modal").find("#data-holder-type").val(),
        Title: $("#add-data-holder-modal").find("#data-holder-title").val(),
        Data: "",
        DataBlockId: g_currentDataBlock.Id
    };

    if (CreateDataHolder(dataHolder) === -1) {
        alert("Ошибка при создании ячейки данных.");
    }
    else {
        $("#add-data-holder-modal").modal("hide");
        RefreshDataHolders();
    }
}

function OnAddImageSubmitButtonClick() {
    let imageModal = $("#add-image-modal");

    let files = imageModal.find("#image-file")[0].files;

    if (files.length == 0) {
        alert("Пожалуйста выберите файл.");
        return;
    }

    let formData = new FormData();
    formData.append("DataBlockId", g_currentDataBlock.Id);
    formData.append("Title", imageModal.find("#image-title").val());
    formData.append("Description", imageModal.find("#image-desc").val());
    formData.append("ImageFile", files[0]);

    if (CreateImage(formData) == -1) {
        alert("Ошибка при создании изображения.");
    }
    else {
        imageModal.modal("hide");
        RefreshImages().then((val) => UpdateImages());
    }
}

function OnAddVideoSubmitButtonClick() {
    if (g_isUploadingVideo) return;

    let videoModal = $("#add-video-modal");

    let files = videoModal.find("#video-file")[0].files;

    if (files.length == 0) {
        alert("Пожалуйста выберите файл.");
        return;
    }

    //File must be smaller than 500MB
    if(files[0].size > 524288000) {
        alert("Размер файла превышает лимит в 500МБ");
        return;
    }

    let formData = new FormData();
    formData.append("DataBlockId", g_currentDataBlock.Id);
    formData.append("Title", videoModal.find("#video-title").val());
    formData.append("Description", videoModal.find("#video-desc").val());
    formData.append("VideoFile", files[0]);

    g_isUploadingVideo = true;
    videoModal.find("#video-file").attr("disabled", "");

    CreateVideo(formData).then(
        (data) => {
            videoModal.modal("hide");
            RefreshVideos().then((val) => UpdateVideos());
            g_isUploadingVideo = false;
            videoModal.find("#video-file").removeAttr("disabled");
        },
        (r) => {
            alert("Ошибка при создании видео.");
            g_isUploadingVideo = false;
            videoModal.find("#video-file").removeAttr("disabled");
        });
}

function OnEditElementButtonClick() {
    switch (g_currentAddButtonActionType) {
        case AddButtonActionTypes.AddDataBlock: {
            let selectedDataBlocks = $("#person-data-block")
                .find(".data-blocks")
                .find(".data-blocks__item input[type=\"checkbox\"]:checked")
                .parents(".data-blocks__item");

            if (selectedDataBlocks.length == 0 ||
                selectedDataBlocks.length > 1) return;

            g_editElementId = selectedDataBlocks.attr("data-id");
            $("#edit-data-block-modal")
                .find("#data-block-title")
                .val(selectedDataBlocks
                        .first()
                        .find(".data-block__value")[0]
                        .innerHTML);

            $("#edit-data-block-modal").modal("show");
            break;
        }
        case AddButtonActionTypes.AddDataHolder: {
            let selectedDataHolders = $("#person-data-block")
                .find(".data-holders")
                .find(".data-holders__item input[type=\"checkbox\"]:checked")
                .parents(".data-holders__item");

            if (selectedDataHolders.length == 0 ||
                selectedDataHolders.length > 1) return;

            g_editElementId = selectedDataHolders.attr("data-id");

            let titleEl = selectedDataHolders
                .first()
                .find(".data-holder__title div")[0];

            if (titleEl == null)
                titleEl = selectedDataHolders
                    .first()
                    .find(".data-holder-gender__title div")[0];

            if (titleEl == null)
                titleEl = selectedDataHolders
                    .first()
                    .find(".data-holder-textarea__title div")[0];

            $("#edit-data-holder-modal")
                .find("#data-holder-title")
                .val(titleEl.innerHTML);

            $("#edit-data-holder-modal").modal("show");
            break;
        }
        case AddButtonActionTypes.AddImage: {
            alert("Edit image modal");
            break;
        }
        case AddButtonActionTypes.AddVideo: {
            alert("Edit video modal");
            break;
        }

        default:
            break;
    }
}

function OnCopyButtonClick() {
    switch (g_currentAddButtonActionType) {
        case AddButtonActionTypes.AddDataBlock: {
            CopySelectedDataBlocks();
            break;
        }
        case AddButtonActionTypes.AddDataHolder: {
            CopySelectedDataHolders();
            break;
        }
        case AddButtonActionTypes.AddImage: {
            CopySelectedImages();
            break;
        }
        case AddButtonActionTypes.AddVideo: {
            CopySelectedVideos();
            break;
        }

        default:
            break;
    }
}

function OnPasteButtonClick() {
    switch (g_currentAddButtonActionType) {
        case AddButtonActionTypes.AddDataBlock: {
            PasteDataBlocks();
            break;
        }
        case AddButtonActionTypes.AddDataHolder: {
            PasteDataHolders();
            break;
        }
        case AddButtonActionTypes.AddImage: {
            PasteImages();
            break;
        }
        case AddButtonActionTypes.AddVideo: {
            PasteVideos();
            break;
        }

        default:
            break;
    }
}

//TODO:
function OnDeleteDataCategoryButtonClick() {

}

function OnDeleteButtonClick() {
    $("#delete-modal").modal("show");
}

function OnDeleteSubmitButtonClick() {
    switch (g_currentAddButtonActionType) {
        case AddButtonActionTypes.AddDataBlock: {
            DeleteSelectedDataBlocks().then((val) => {
                RefreshDataBlocks();
                UpdateDataBlocks();
                $("#delete-modal").modal("hide");
            });
            break;
        }
        case AddButtonActionTypes.AddDataHolder: {
            DeleteSelectedDataHolders().then((val) => {
                RefreshDataHolders();
                UpdateDataHolders();
                $("#delete-modal").modal("hide");
            });
            break;
        }
        case AddButtonActionTypes.AddImage: {
            DeleteSelectedImages().then((val) => {
                RefreshImages().then((val) => {
                    UpdateImages();
                    $("#delete-modal").modal("hide");
                });
            });            
            break;
        }
        case AddButtonActionTypes.AddVideo: {
            DeleteSelectedVideos().then((val) => {
                RefreshVideos().then((val) => {
                    UpdateVideos();
                    $("#delete-modal").modal("hide");
                });                
            });
            break;
        }

        default:
            break;
    }    
}

function OnSaveButtonClick() {
    if (g_isSaving) return;
    if ($("#person-data-block").find(".data-holders .data-holders__item").length === 0) return;
    g_isSaving = true;

    SaveData();
}

async function SaveData() {
    let saveButton = $("#person-data-block #save-button");
    saveButton.find(".loader").css("display", "block");
    saveButton.find(".btn__text")[0].innerHTML = "Сохранение";

    if (SaveDataHolders()) {
        saveButton.find(".loader").css("display", "none");
        saveButton.find(".btn__text")[0].innerHTML = "Сохранено";
        saveButton.removeClass("btn-default");
        saveButton.addClass("btn-success");
        await WaitForMilliseconds(1500);

        g_currentDataCategory = GetDataCategory(g_currentDataCategory.Id);

        if (g_currentDataCategory.DataCategoryType == DataCategoryTypes.PersonInfo)
            ReloadTree($("#mainPerson")[0].getAttribute("data-value"));
    }
    else {
        alert("Произошла ошибка во время сохранения.");
    }

    saveButton.find(".btn__text")[0].innerHTML = "Сохранить";
    saveButton.removeClass("btn-success");
    saveButton.addClass("btn-default");

    g_isSaving = false;
}

function OnEditPrivacyButtonClick() {  
    switch (g_currentAddButtonActionType) {
        case AddButtonActionTypes.AddDataHolder: {
            let selectedDataHolders = $("#person-data-block")
                .find(".data-holders")
                .find(".data-holders__item input[type=\"checkbox\"]:checked")
                .parents(".data-holders__item");
            
            if (selectedDataHolders.length == 0 ||
                selectedDataHolders.length > 1) return;

            let dataHolderId = selectedDataHolders.attr("data-id");
            g_editPrivacyElementId = dataHolderId;
            LoadDataHolderPrivacyData(dataHolderId);
            break;
        }

        case AddButtonActionTypes.AddImage: {
            let selectedImages = [];

            if (selectedImages.length == 0 ||
                selectedImages.length > 1) return;

            let imageId = selectedImages.attr("data-id");
            g_editPrivacyElementId = imageId;
            LoadImagePrivacyData(imageId);
            break;
        }

        case AddButtonActionTypes.AddVideo: {
            let selectedVideos = [];

            if (selectedVideos.length == 0 ||
                selectedVideos.length > 1) return;

            let videoId = selectedVideos.attr("data-id");
            g_editPrivacyElementId = videoId;
            LoadVideoPrivacyData(videoId);
            break;
        }
            
        default:
            return;
    }

    $("#privacy-level-modal").modal("show");
}

function OnEditDataCategorySubmitButtonClick() {
    let dataCategory = {
        id: g_editElementId,
        command: {
            Id: g_editElementId,
            Name: $("#edit-data-category-modal")
                      .find("#data-category-name")
                      .val()
        }
    };

    if (!UpdateDataCategoryName(dataCategory)) {
        alert("Ошибка при изменении имени категории.");
    }
    else {
        $("#edit-data-category-modal").modal("hide");
        LoadPersonData(g_currentPersonId);
    }
}

function OnEditDataBlockSubmitButtonClick() {
    let dataBlock = {
        id: g_editElementId,
        command: {
            Id: g_editElementId,
            Title: $("#edit-data-block-modal")
                      .find("#data-block-title")
                      .val()
        }
    };

    if (!UpdateDataBlockTitle(dataBlock)) {
        alert("Ошибка при изменении заголовка блока для данных.");
    }
    else {
        $("#edit-data-block-modal").modal("hide");
        RefreshDataBlocks();
    }
}

function OnEditDataHolderSubmitButtonClick() {
    let dataHolder = {
        id: g_editElementId,
        command: {
            Id: g_editElementId,
            Title: $("#edit-data-holder-modal")
                      .find("#data-holder-title")
                      .val()
        }
    };

    if (!UpdateDataHolderTitle(dataHolder)) {
        alert("Ошибка при изменении заголовка ячейки для данных.");
    }
    else {
        $("#edit-data-holder-modal").modal("hide");
        RefreshDataHolders();
    }
}

function OnSliderArrowClick() {
    let slider = $("#image-carousel-modal")
        .find(".slider");

    let imageId = slider
        .find(".slick-active")
        .attr("data-id");

    UpdateSliderImageDetails(imageId);
}

function OnSaveImageSubmitButtonClick() {
    let currentImageId = GetImageSliderCurrentImageId();
    let sliderModal = $("#image-carousel-modal");

    let image = {
        Id: currentImageId,
        Title: sliderModal.find("#slider-image-title").val(),
        Description: sliderModal.find("#slider-image-desc").val()
    };

    if (!UpdateImageDetails(image)) {
        alert("Ошибка при сохранении данных изображения.");
    }
    else {
        RefreshImages().then(val => UpdateSliderImageDetails(currentImageId));
    }
}

function OnSetImageAsAvatarButtonClick() {
    if (!UpdatePersonAvatarImage(g_currentPersonId, GetImageSliderCurrentImageId())) {
        alert("Ошибка при задании изображения аватара персоны.");
    }
    else {
        ReloadTree(_currentFamilyTree.MainPersonId);
    }
}

function OnVideoModalVideoClick(event) {    
    let videosListItemElement = $(event.currentTarget);
    let videoId = videosListItemElement.attr("data-id");

    UpdateVideoModal(videoId);

    SelectVideoModalVideo(videoId);
}

function OnSaveVideoSubmitButtonClick() {
    let currentVideoId = GetVideoModalCurrentVideoId();
    let videoModal = $("#video-modal");

    let video = {
        Id: currentVideoId,
        Title: videoModal.find("#current-video-title").val(),
        Description: videoModal.find("#current-video-desc").val()
    };

    if (!UpdateVideoDetails(video)) {
        alert("Ошибка при сохранении данных видео.");
    }
    else {
        RefreshVideos().then((val) => {
            UpdateVideoModalVideos();
            SelectVideoModalVideo(currentVideoId);
        });
    }
}

//UI
function UpdateDataBlocks() {
    ClearDataBlocks();

    g_currentDataCategory
        .DataBlocks
        .forEach((item) => {
            AddItemToDataBlocks(item);
        });

    new Sortable($(".person-data-block__data-blocks")[0], {
        handle: ".data-block__selector",
        animation: 500,
        onEnd: (event) => {
            let dataBlock = {
                id: $(event.item).attr("data-id"),
                command: {
                    Id: $(event.item).attr("data-id"),
                    Order: event.newIndex + 1
                }
            };

            UpdateDataBlockOrder(dataBlock);
        }
    });

    $("#person-data-block")
        .find(".data-blocks")
        .find(".data-blocks__item")
        .click(OnDataBlockClick);
}

function UpdateDataHolders() {
    ClearDataHolders();

    if (g_currentDataBlock == null)
        return;

    g_currentDataBlock
        .DataHolders
        .forEach((item) => {
            AddItemToDataHolders(item);
        });

    new Sortable($(".person-data-block__data-holders")[0], {
        handle: ".data-holder__selector, .data-holder-gender__selector, .data-holder-textarea__selector",
        animation: 500,
        onEnd: (event) => {
            let dataHolder = {
                id: $(event.item).attr("data-id"),
                command: {
                    Id: $(event.item).attr("data-id"),
                    Order: event.newIndex + 1
                }
            };

            UpdateDataHolderOrder(dataHolder);
        }
    });
}

function UpdateImages() {
    ClearImages();

    if (g_currentDataBlockImages == null)
        return;

    g_currentDataBlockImages
        .forEach((item) => {
            AddItemToImages(item);
        });

    $("#person-data-block")
        .find(".images .images__item")
        .click(OnImageClick);
}

function UpdateImageSlider(imageId) {
    let slider = $("#image-carousel-modal")
        .find(".slider");

    if (slider.hasClass("slick-initialized")) {
        slider.slick("unslick");
        ClearSliderImages();
    }        

    if (g_currentDataBlockImages == null)
        return;

    g_currentDataBlockImages
        .forEach((item) => {
            AddImageToSlider(item);
        });

    let initialSlide = 0;

    let selectedImage = g_currentDataBlockImages
        .find(item => item.Id == imageId);

    initialSlide = g_currentDataBlockImages.indexOf(selectedImage);

    slider.slick({
        slidesToScroll: 1,
        slidesToShow: 1,
        draggable: false,
        arrows: true,
        variableWidth: true
    });

    slider
        .find(".slick-arrow")
        .click(OnSliderArrowClick);    

    slider.slick("slickGoTo", initialSlide, false);

    UpdateSliderImageDetails(imageId);
}

function UpdateSliderImageDetails(imageId) {
    let sliderModal = $("#image-carousel-modal");

    let image = g_currentDataBlockImages
        .find(item => item.Id == imageId);

    sliderModal
        .find("#slider-image-title")
        .val(image.Title);

    sliderModal
        .find("#slider-image-desc")
        .val(image.Description);

    sliderModal
        .find(".pages__current-page")
        .text(sliderModal.find(".slider").slick("slickCurrentSlide") + 1);

    sliderModal
        .find(".pages__count")
        .text(g_currentDataBlockImages.length);
}

function UpdateVideos() {
    ClearVideos();

    if (g_currentDataBlockVideos == null)
        return;

    g_currentDataBlockVideos
        .forEach((item) => {
            AddItemToVideos(item);
        });

    $("#person-data-block")
        .find(".videos .videos__item")
        .click(OnVideoClick);
}

function UpdateVideoModal(videoId) {
    let videoModal = $("#video-modal");
    let currentVideoElement = videoModal.find("#current-video")[0];

    let currentVideo = g_currentDataBlockVideos
        .find((item) => item.Id == videoId);

    videoModal.find("#current-video-title").val(currentVideo.Title);
    videoModal.find("#current-video-desc").val(currentVideo.Description);

    currentVideoElement.poster = "data:image/" + currentVideo.PreviewImageFormat + ";base64," + currentVideo.PreviewImageData;
    currentVideoElement.src = "Media/GetVideo/" + videoId;
    currentVideoElement.volume = 0.5;
    currentVideoElement.load();
}

function UpdateVideoModalVideos() {
    ClearVideoModalVideos();

    if (g_currentDataBlockVideos == null)
        return;

    g_currentDataBlockVideos
        .forEach((item) => {
            AddVideoToVideoModal(item);
        });

    $("#video-modal .videos-list .videos-list__item")
        .click(OnVideoModalVideoClick);
}

function RefreshDataBlocks() {
    $("#person-data-block")
        .find(".data-categories")
        .find(".data-categories__item[data-id=\"" + g_currentDataCategory.Id + "\"]")
        .click();
}

function RefreshDataHolders() {
    $("#person-data-block")
        .find(".data-categories")
        .find(".data-categories__item[data-id=\"" + g_currentDataCategory.Id + "\"]")
        .click();
    $("#person-data-block")
        .find(".data-blocks")
        .find(".data-blocks__item[data-id=\"" + g_currentDataBlock.Id + "\"]")
        .click();
}

async function RefreshImages() {
    g_currentDataBlockImages = await GetImages(g_currentDataBlock.Id);
}

async function RefreshVideos() {
    g_currentDataBlockVideos = await GetVideos(g_currentDataBlock.Id);
}

function OpenDefaultDataBlockTab() {
    $("#person-data-block")
        .find(".data-block-content")
        .find(".tabs-buttons")
        .find(".tab-button-data")
        .click();
}

function ShowBackToDataBlocksButton(isShow = true) {
    $("#person-data-block").find("#back-to-data-blocks-button")[0].style.display = isShow ? "inline-block" : "none";
}

function ShowDataBlocks(isShow = true) {
    $("#person-data-block").find(".data-blocks")[0].style.display = isShow ? "block" : "none";
}

function ShowDataBlockContent(isShow = true) {
    $("#person-data-block").find(".data-block-content")[0].style.display = isShow ? "block" : "none";
}

function ShowDataBlockContentTab(dataBlockContentTab) {
    let dataBlockContentTabsContainersElement = $("#person-data-block")
        .find(".data-block-content")
        .find(".tabs-containers");

    dataBlockContentTabsContainersElement
        .children()
        .css("display", "none");

    switch (dataBlockContentTab) {
        case DataBlockContentTabs.Data: {
            dataBlockContentTabsContainersElement
                .find(".data-holders")
                .css("display", "block");
            break;
        }
        case DataBlockContentTabs.Images: {
            dataBlockContentTabsContainersElement
                .find(".images")
                .css("display", "block");
            break;
        }
        case DataBlockContentTabs.Videos: {
            dataBlockContentTabsContainersElement
                .find(".videos")
                .css("display", "block");
            break;
        }

        default:
            break;
    }
}

function ShowSaveButton(isShow = true) {
    $("#person-data-block")
        .find("#save-button")
        .css("display", isShow ? "block" : "none");
}

function ClearDataCategories() {
    $("#person-data-block").find(".data-categories").empty();
}

function ClearDataBlocks() {
    $("#person-data-block").find(".data-blocks").empty();
}

function ClearDataHolders() {
    $("#person-data-block").find(".data-holders").empty();
}

function ClearImages() {
    $("#person-data-block").find(".images").empty();
}

function ClearVideos() {
    $("#person-data-block").find(".videos").empty();
}

function ClearSliderImages() {
    $("#image-carousel-modal")
        .find(".slider")
        .empty();
}

function ClearVideoModalVideos() {
    $("#video-modal .videos-list").empty();
}

function AddItemToDataCategories(dataCategory) {
    let dataCategoryElement = document.createElement("div");
    dataCategoryElement.classList.add("data-categories__item");
    dataCategoryElement.setAttribute("data-id", dataCategory.Id);
    dataCategoryElement.innerHTML = dataCategory.Name;

    $("#person-data-block")
        .find(".data-categories")[0]
        .appendChild(dataCategoryElement);
}

function AddItemToDataBlocks(dataBlock) {
    let dataBlockElement = document.createElement("div");
    dataBlockElement.classList.add("data-blocks__item");
    dataBlockElement.classList.add("data-block");
    dataBlockElement.setAttribute("data-id", dataBlock.Id);

    let dataBlockHeaderElement = document.createElement("div");
    dataBlockHeaderElement.classList.add("data-block__header");
    let dataBlockFooterElement = document.createElement("div");
    dataBlockFooterElement.classList.add("data-block__footer");
    let dataBlockBodyElement = document.createElement("div");
    dataBlockBodyElement.classList.add("data-block__body");

    let dataBlockSelectorElement = document.createElement("div");
    dataBlockSelectorElement.classList.add("data-block__selector");
    let checkboxElement = document.createElement("div");
    checkboxElement.classList.add("checkbox");
    let checkboxInputElement = document.createElement("input");
    checkboxInputElement.type = "checkbox";
    checkboxElement.appendChild(checkboxInputElement);
    dataBlockSelectorElement.appendChild(checkboxElement);

    let dataBlockContentElement = document.createElement("div");
    dataBlockContentElement.classList.add("data-block__content");

    let dataBlockItemElement = document.createElement("div");
    dataBlockItemElement.classList.add("data-block__item");

    let dataBlockTitleElement = document.createElement("div");
    dataBlockTitleElement.classList.add("data-block__title");
    dataBlockTitleElement.innerHTML = "Заголовок:";

    let dataBlockValueElement = document.createElement("div");
    dataBlockValueElement.classList.add("data-block__value");
    dataBlockValueElement.innerHTML = dataBlock.Title;

    dataBlockItemElement.appendChild(dataBlockTitleElement);
    dataBlockItemElement.appendChild(dataBlockValueElement);

    dataBlockContentElement.appendChild(dataBlockItemElement);

    dataBlockBodyElement.appendChild(dataBlockSelectorElement);
    dataBlockBodyElement.appendChild(dataBlockContentElement);

    dataBlockElement.appendChild(dataBlockHeaderElement);
    dataBlockElement.appendChild(dataBlockBodyElement);
    dataBlockElement.appendChild(dataBlockFooterElement);

    $("#person-data-block")
        .find(".data-blocks")[0]
        .appendChild(dataBlockElement);
}

function AddItemToDataHolders(dataHolder) {
    let dataHolderElement = null;

    switch (dataHolder.DataHolderType) {
        case DataHolderTypes.Text : {
            dataHolderElement = CreateTextDataHolderElement(dataHolder);
            break;
        }        
        case DataHolderTypes.TextArea: {
            dataHolderElement = CreateTextAreaDataHolderElement(dataHolder);
            break;
        }
        case DataHolderTypes.Date : {
            dataHolderElement = CreateDateDataHolderElement(dataHolder);
            break;
        }
        case DataHolderTypes.DateTime : {
            dataHolderElement = CreateDateTimeDataHolderElement(dataHolder);
            break;
        }
        case DataHolderTypes.Time : {
            dataHolderElement = CreateTimeDataHolderElement(dataHolder);
            break;
        }
        case DataHolderTypes.Name: {
            dataHolderElement = CreateTextDataHolderElement(dataHolder);
            break;
        }
        case DataHolderTypes.Surname: {
            dataHolderElement = CreateTextDataHolderElement(dataHolder);
            break;
        }
        case DataHolderTypes.MiddleName: {
            dataHolderElement = CreateTextDataHolderElement(dataHolder);
            break;
        }
        case DataHolderTypes.Birthday: {
            dataHolderElement = CreateDateDataHolderElement(dataHolder);
            break;
        }
        case DataHolderTypes.Gender: {
            dataHolderElement = CreateGenderDataHolderElement(dataHolder);
            break;
        }
        default:
            return;
    }

    $("#person-data-block")
        .find(".data-holders")[0]
        .appendChild(dataHolderElement);
}

function AddItemToImages(image) {
    let imageElement = document.createElement("div");
    imageElement.classList.add("image");
    imageElement.classList.add("images__item");
    imageElement.setAttribute("data-id", image.Id);

    let selectorElement = document.createElement("div");
    selectorElement.classList.add("image__selector");

    let checkboxElement = document.createElement("div");
    checkboxElement.classList.add("checkbox");

    let inputElement = document.createElement("input");
    inputElement.type = "checkbox";

    checkboxElement.appendChild(inputElement);
    selectorElement.appendChild(checkboxElement);

    let imgElement = document.createElement("img");
    imgElement.src = "data:image/" + image.ImageFormat + ";base64," + image.ImageData;

    imageElement.appendChild(selectorElement);
    imageElement.appendChild(imgElement);

    $("#person-data-block")
        .find(".images")[0]
        .appendChild(imageElement);
}

function AddItemToVideos(video) {
    let videoElement = document.createElement("div");
    videoElement.classList.add("video");
    videoElement.classList.add("videos__item");
    videoElement.setAttribute("data-id", video.Id);

    let selectorElement = document.createElement("div");
    selectorElement.classList.add("video__selector");

    let checkboxElement = document.createElement("div");
    checkboxElement.classList.add("checkbox");

    let inputElement = document.createElement("input");
    inputElement.type = "checkbox";

    checkboxElement.appendChild(inputElement);
    selectorElement.appendChild(checkboxElement);

    let imgElement = document.createElement("img");
    imgElement.src = "data:image/" + video.PreviewImageFormat + ";base64," + video.PreviewImageData;

    videoElement.appendChild(selectorElement);
    videoElement.appendChild(imgElement);

    $("#person-data-block")
        .find(".videos")[0]
        .appendChild(videoElement);
}

function AddImageToSlider(image) {
    let slider = $("#image-carousel-modal")
        .find(".slider")[0];

    let imgElement = document.createElement("img");
    imgElement.src = "data:image/" + image.ImageFormat + ";base64," + image.ImageData;
    imgElement.setAttribute("data-id", image.Id);

    slider.appendChild(imgElement);
}

function AddVideoToVideoModal(video) {
    let videosList = $("#video-modal .videos-list");

    let videosLitsItemElement = $(document.createElement("div"));
    videosLitsItemElement.addClass("videos-list__item");
    videosLitsItemElement.addClass("video-info");
    videosLitsItemElement.attr("data-id", video.Id);

    let videoPreviewImage = $(document.createElement("div"));
    videoPreviewImage.addClass("video-info__preview-image");

    let imgElement = document.createElement("img");
    imgElement.src = "data:image/" + video.PreviewImageFormat + ";base64," + video.PreviewImageData;

    videoPreviewImage.append(imgElement);

    let videoTitleElement = document.createElement("div");
    videoTitleElement.classList.add("video-info__title");
    videoTitleElement.innerHTML = video.Title;

    videosLitsItemElement.append(videoPreviewImage);
    videosLitsItemElement.append(videoTitleElement);

    videosList.append(videosLitsItemElement);
}

function CreateDataHolderElement(dataHolder) {
    let dataHolderElement = document.createElement("div");
    dataHolderElement.classList.add("data-holders__item");
    dataHolderElement.classList.add("data-holder");
    dataHolderElement.setAttribute("data-id", dataHolder.Id);
    return dataHolderElement;
}

function CreateDataHolderSelectorElement(dataHolder) {
    let dataHolderSelectorElement = document.createElement("div");
    dataHolderSelectorElement.classList.add("data-holder__selector");

    if (dataHolder.IsDeletable) {
        let checkboxElement = document.createElement("div");
        checkboxElement.classList.add("checkbox");
        let checkboxInputElement = document.createElement("input");
        checkboxInputElement.type = "checkbox";

        checkboxElement.appendChild(checkboxInputElement);
        dataHolderSelectorElement.appendChild(checkboxElement);
    }    

    return dataHolderSelectorElement;
}

function CreateDataHolderTitleElement(dataHolder) {
    let dataHolderTitleElement = document.createElement("div");
    dataHolderTitleElement.classList.add("data-holder__title");
    let titleElement = document.createElement("div");
    titleElement.innerHTML = dataHolder.Title;
    dataHolderTitleElement.appendChild(titleElement);

    return dataHolderTitleElement;
}

function CreateDataHolderDataElement() {
    let dataHolderDataElement = document.createElement("div");
    dataHolderDataElement.classList.add("data-holder__data");

    return dataHolderDataElement;
}

function CreateDataHolderPrivacyElement(dataHolder) {
    let dataHolderPrivacyElement = document.createElement("div");
    dataHolderPrivacyElement.classList.add("data-holder__privacy");
    dataHolderPrivacyElement.setAttribute("data-toggle", "tooltip");
    dataHolderPrivacyElement.setAttribute("data-placement", "right");

    let title = "";

    if (dataHolder.Privacy == null) {
        title = "Личный";
        dataHolderPrivacyElement.classList.add("privacy-confidential");
    }
    else {
        switch (dataHolder.Privacy.PrivacyLevel) {
            case PrivacyLevels.Confidential: {
                title = "Личный";
                dataHolderPrivacyElement.classList.add("privacy-confidential");
                break;
            }

            case PrivacyLevels.InternalUse: {
                title = "Внутренний";
                dataHolderPrivacyElement.classList.add("privacy-internal-use");
                break;
            }

            case PrivacyLevels.PublicUse: {
                title = "Публичный";
                dataHolderPrivacyElement.classList.add("privacy-public-use");
                break;
            }

            case PrivacyLevels.TopSecret: {
                title = "Строго сикретно";
                dataHolderPrivacyElement.classList.add("privacy-top-secret");
                break;
            }

            default:
                break;
        }
    }

    dataHolderPrivacyElement.setAttribute("title", title);

    return dataHolderPrivacyElement;
}

function CreateTextDataHolderElement(dataHolder) {
    let dataHolderElement = CreateDataHolderElement(dataHolder);  

    let dataHolderDataElement = CreateDataHolderDataElement();
    let inputElement = document.createElement("input");
    inputElement.type = "text";
    inputElement.value = dataHolder.Data;
    dataHolderDataElement.appendChild(inputElement);

    dataHolderElement.appendChild(CreateDataHolderSelectorElement(dataHolder));
    dataHolderElement.appendChild(CreateDataHolderTitleElement(dataHolder));
    dataHolderElement.appendChild(dataHolderDataElement);
    dataHolderElement.appendChild(CreateDataHolderPrivacyElement(dataHolder));

    return dataHolderElement;
}

function CreateTextAreaDataHolderElement(dataHolder) {
    let dataHolderElement = CreateDataHolderElement(dataHolder);
    dataHolderElement.classList.replace("data-holder", "data-holder-textarea");

    let dataHolderSelectorElement = CreateDataHolderSelectorElement(dataHolder);
    dataHolderSelectorElement.classList.replace("data-holder__selector", "data-holder-textarea__selector");

    let dataHolderTitleElement = CreateDataHolderTitleElement(dataHolder);
    dataHolderTitleElement.classList.replace("data-holder__title", "data-holder-textarea__title");

    let dataHolderDataElement = CreateDataHolderDataElement();
    dataHolderDataElement.classList.replace("data-holder__data", "data-holder-textarea__data");

    let textAreaElement = document.createElement("textarea");
    textAreaElement.value = dataHolder.Data;
    textAreaElement.setAttribute("rows", "6");
    dataHolderDataElement.appendChild(textAreaElement);

    let dataHolderPrivacyElement = CreateDataHolderPrivacyElement(dataHolder);
    dataHolderPrivacyElement.classList.replace("data-holder__privacy", "data-holder-textarea__privacy");

    dataHolderElement.appendChild(dataHolderSelectorElement);
    dataHolderElement.appendChild(dataHolderTitleElement);
    dataHolderElement.appendChild(dataHolderPrivacyElement);
    dataHolderElement.appendChild(dataHolderDataElement);

    return dataHolderElement;
}

function CreateDateDataHolderElement(dataHolder) {
    let dataHolderElement = CreateDataHolderElement(dataHolder);

    let dataHolderDataElement = CreateDataHolderDataElement();
    let inputElement = document.createElement("input");
    inputElement.type = "date";
    if (dataHolder.Data == "") {
        inputElement.value = 0;
    }
    else {
        inputElement.value = new Date(dataHolder.Data)
            .toISOString()
            .split('T')[0];
    }
    dataHolderDataElement.appendChild(inputElement);

    dataHolderElement.appendChild(CreateDataHolderSelectorElement(dataHolder));
    dataHolderElement.appendChild(CreateDataHolderTitleElement(dataHolder));
    dataHolderElement.appendChild(dataHolderDataElement);
    dataHolderElement.appendChild(CreateDataHolderPrivacyElement(dataHolder));

    return dataHolderElement;
}

function CreateDateTimeDataHolderElement(dataHolder) {
    let dataHolderElement = CreateDataHolderElement(dataHolder);

    let dataHolderDataElement = CreateDataHolderDataElement();
    let inputElement = document.createElement("input");
    inputElement.type = "datetime-local";
    inputElement.value = dataHolder.Data;
    dataHolderDataElement.appendChild(inputElement);

    dataHolderElement.appendChild(CreateDataHolderSelectorElement(dataHolder));
    dataHolderElement.appendChild(CreateDataHolderTitleElement(dataHolder));
    dataHolderElement.appendChild(dataHolderDataElement);
    dataHolderElement.appendChild(CreateDataHolderPrivacyElement(dataHolder));

    return dataHolderElement;
}

function CreateTimeDataHolderElement(dataHolder) {
    let dataHolderElement = CreateDataHolderElement(dataHolder);

    let dataHolderDataElement = CreateDataHolderDataElement();
    let inputElement = document.createElement("input");
    inputElement.type = "time";
    inputElement.value = dataHolder.Data;
    dataHolderDataElement.appendChild(inputElement);

    dataHolderElement.appendChild(CreateDataHolderSelectorElement(dataHolder));
    dataHolderElement.appendChild(CreateDataHolderTitleElement(dataHolder));
    dataHolderElement.appendChild(dataHolderDataElement);
    dataHolderElement.appendChild(CreateDataHolderPrivacyElement(dataHolder));

    return dataHolderElement;
}

function CreateGenderDataHolderElement(dataHolder) {
    let dataHolderElement = CreateDataHolderElement(dataHolder);
    dataHolderElement.classList.replace("data-holder", "data-holder-gender");

    let dataHolderSelectorElement = CreateDataHolderSelectorElement(dataHolder);
    dataHolderSelectorElement.classList.replace("data-holder__selector", "data-holder-gender__selector");

    let dataHolderTitleElement = CreateDataHolderTitleElement(dataHolder);
    dataHolderTitleElement.classList.replace("data-holder__title", "data-holder-gender__title");

    let dataHolderDataElement = CreateDataHolderDataElement();
    dataHolderDataElement.classList.replace("data-holder__data", "data-holder-gender__data");

    let buttonGroupElement = document.createElement("div");
    buttonGroupElement.classList.add("btn-group");
    buttonGroupElement.classList.add("btn-group-toggle");
    buttonGroupElement.setAttribute("data-toggle", "buttons");

    let labelsElements = [];
    labelsElements.push(document.createElement("label"));
    labelsElements.push(document.createElement("label"));
    labelsElements.push(document.createElement("label"));

    $(labelsElements).addClass("btn").addClass("btn-default");

    let inputElements = [];
    inputElements.push(document.createElement("input"));
    inputElements.push(document.createElement("input"));
    inputElements.push(document.createElement("input"));

    inputElements[0].type = "radio";
    inputElements[0].name = "person-gender";
    inputElements[0].value = "Male";

    inputElements[1].type = "radio";
    inputElements[1].name = "person-gender";
    inputElements[1].value = "Female";

    inputElements[2].type = "radio";
    inputElements[2].name = "person-gender";
    inputElements[2].value = "Unknown";

    labelsElements[0].appendChild(inputElements[0]);
    labelsElements[0].innerHTML += "Мужчина";
    labelsElements[1].appendChild(inputElements[1]);
    labelsElements[1].innerHTML += "Женщина";
    labelsElements[2].appendChild(inputElements[2]);
    labelsElements[2].innerHTML += "Неизвестно";

    buttonGroupElement.appendChild(labelsElements[0]);
    buttonGroupElement.appendChild(labelsElements[1]);
    buttonGroupElement.appendChild(labelsElements[2]);

    dataHolderDataElement.appendChild(buttonGroupElement);

    let dataHolderPrivacyElement = CreateDataHolderPrivacyElement(dataHolder);
    dataHolderPrivacyElement.classList.replace("data-holder__privacy", "data-holder-gender__privacy");

    dataHolderElement.appendChild(dataHolderSelectorElement);
    dataHolderElement.appendChild(dataHolderTitleElement);
    dataHolderElement.appendChild(dataHolderDataElement);
    dataHolderElement.appendChild(dataHolderPrivacyElement);

    switch (dataHolder.Data) {
        case GenderTypes.Male: {
            inputElements[0].setAttribute("checked", "");
            labelsElements[0].classList.add("active");
            break;
        }
        case GenderTypes.Female: {
            inputElements[1].setAttribute("checked", "");
            labelsElements[1].classList.add("active");
            break;
        }
        case GenderTypes.Unknown: {
            inputElements[2].setAttribute("checked", "");
            labelsElements[2].classList.add("active");
            break;
        }

        default:
            break;
    }

    return dataHolderElement;
}

function GetSelectedDataBlocksIds() {
    let result = [];

    $("#person-data-block")
        .find(".data-blocks .data-blocks__item")
        .each((i, el) => {
            if ($(el).find("input[type=\"checkbox\"]").is(":checked")) {
                result.push(el.getAttribute("data-id"));
            }
        });

    return result;
}

function GetSelectedDataHoldersIds() {
    let result = [];

    $("#person-data-block")
        .find(".data-holders .data-holders__item")
        .each((i, el) => {
            if ($(el).find("input[type=\"checkbox\"]").is(":checked")) {
                result.push(el.getAttribute("data-id"));
            }
        });

    return result;
}

function GetSelectedImagesIds() {
    let result = [];

    $("#person-data-block")
        .find(".images .images__item")
        .each((i, el) => {
            if ($(el).find("input[type=\"checkbox\"]").is(":checked")) {
                result.push(el.getAttribute("data-id"));
            }
        });

    return result;
}

function GetSelectedVideosIds() {
    let result = [];

    $("#person-data-block")
        .find(".videos .videos__item")
        .each((i, el) => {
            if ($(el).find("input[type=\"checkbox\"]").is(":checked")) {
                result.push(el.getAttribute("data-id"));
            }
        });

    return result;
}

function GetImageSliderCurrentImageId() {
    return $("#image-carousel-modal")
        .find(".slider .slick-current")
        .attr("data-id");
}

function GetVideoModalCurrentVideoId() {
    return $("#video-modal .videos-list .videos-list__item_active")
        .attr("data-id");
}

function CopySelectedDataBlocks() {    
    g_copyObject = {
        Ids: GetSelectedDataBlocksIds(),
        CopyObjectType: CopyObjectTypes.DataBlock
    };
}

function CopySelectedDataHolders() {
    g_copyObject = {
        Ids: GetSelectedDataHoldersIds(),
        CopyObjectType: CopyObjectTypes.DataHolder
    };
}

function CopySelectedImages() {
    g_copyObject = {
        Ids: GetSelectedImagesIds(),
        CopyObjectType: CopyObjectTypes.Image
    };
}

function CopySelectedVideos() {
    g_copyObject = {
        Ids: GetSelectedVideosIds(),
        CopyObjectType: CopyObjectTypes.Video
    };
}

function PasteDataBlocks() {
    if (g_copyObject == null ||
        g_copyObject.Ids.length == 0)
        return;

    if (g_copyObject.CopyObjectType == null ||
        g_copyObject.CopyObjectType != CopyObjectTypes.DataBlock) {
        alert("Ошибка при вставке из буфера (неверный тип объектов)");
        return;
    }

    if (!CopyDataBlocks(g_copyObject.Ids, g_currentDataCategory.Id)) {
        alert("Ошибка при вставке из буфера");
        return;
    }

    RefreshDataBlocks();
    UpdateDataBlocks();
}

function PasteDataHolders() {
    if (g_copyObject == null ||
        g_copyObject.Ids.length == 0)
        return;

    if (g_copyObject.CopyObjectType == null ||
        g_copyObject.CopyObjectType != CopyObjectTypes.DataHolder) {
        alert("Ошибка при вставке из буфера (неверный тип объектов)");
        return;
    }

    if (!CopyDataHolders(g_copyObject.Ids, g_currentDataBlock.Id)) {
        alert("Ошибка при вставке из буфера");
        return;
    }

    RefreshDataHolders();
    UpdateDataHolders();
}

function PasteImages() {
    if (g_copyObject == null ||
        g_copyObject.Ids.length == 0)
        return;

    if (g_copyObject.CopyObjectType == null ||
        g_copyObject.CopyObjectType != CopyObjectTypes.Image) {
        alert("Ошибка при вставке из буфера (неверный тип объектов)");
        return;
    }

    CopyImages(g_copyObject.Ids, g_currentDataBlock.Id)
        .then((data) => {
            RefreshImages().then((val) => UpdateImages());
        },
        (r) => { 
            alert("Ошибка при вставке из буфера");
        });    
}

function PasteVideos() {
    if (g_copyObject == null ||
        g_copyObject.Ids.length == 0)
        return;

    if (g_copyObject.CopyObjectType == null ||
        g_copyObject.CopyObjectType != CopyObjectTypes.Video) {
        alert("Ошибка при вставке из буфера (неверный тип объектов)");
        return;
    }

    CopyVideos(g_copyObject.Ids, g_currentDataBlock.Id)
        .then((data) => {
            RefreshVideos().then((val) => UpdateVideos());
        },
        (r) => {
            alert("Ошибка при вставке из буфера");
        });  
}

function SelectVideoModalVideo(videoId) {
    let videosListElement = $("#video-modal .videos-list");

    videosListElement
        .children()
        .removeClass("videos-list__item_active");

    videosListElement
        .find(".videos-list__item[data-id=\"" + videoId + "\"]")
        .addClass("videos-list__item_active");
}

async function DeleteSelectedDataBlocks() {
    let dataBlocksIds = GetSelectedDataBlocksIds();

    if (dataBlocksIds.length == 0)
        return;

    const promises = dataBlocksIds.map(DeleteDataBlock);

    await Promise.all(promises);
}

async function DeleteSelectedDataHolders() {
    let dataHoldersIds = GetSelectedDataHoldersIds();

    if (dataHoldersIds.length == 0)
        return;

    const promises = dataHoldersIds.map(DeleteDataHolder);

    await Promise.all(promises);
}

async function DeleteSelectedImages() {
    let imagesIds = GetSelectedImagesIds();

    if (imagesIds.length == 0)
        return;

    const promises = imagesIds.map(DeleteImage);

    await Promise.all(promises);
}

async function DeleteSelectedVideos() {
    let videosIds = GetSelectedVideosIds();

    if (videosIds.length == 0)
        return;
    
    const promises = videosIds.map(DeleteVideo);

    await Promise.all(promises);
}