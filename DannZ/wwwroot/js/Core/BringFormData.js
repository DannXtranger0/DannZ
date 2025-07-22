export function bringFormData(form, isObject = true) {
    let formData = new FormData(form);
    if (isObject)
        formData =Object.fromEntries(formData.entries());
    return formData
}