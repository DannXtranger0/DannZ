export function asignImage(id, file, fallback = "") {
    let elem = document.getElementById(id);
    if (file != null)
        elem.src = file; 
}

export function asignContentField(id, content){
    let elem = document.getElementById(id);
    elem.textContent = content ?? "";
    elem.value = content ?? "";
}
