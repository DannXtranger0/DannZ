export function bringFormData(form, isObject = true) {
    let formData = new FormData(form);
    if (isObject)
        formData =Object.fromEntries(formData.entries());
    return formData
}

export function getUserId() {
    return  document.getElementById("userIdHidden").dataset.id;
}