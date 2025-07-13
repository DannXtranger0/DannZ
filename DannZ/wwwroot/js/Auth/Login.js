let form = document.querySelector("form");
let rememberMe = document.querySelector("[name='RememberMe']");
form.addEventListener("submit", login);

async function login(event) {

    event.preventDefault();

    let formData = new FormData(form);
    let formObject = Object.fromEntries(formData.entries());

    formObject["RememberMe"] = rememberMe.checked;

    try {

        let response = await fetch("https://localhost:7238/api/Auth", {
            method: 'POST',
            headers: {
                'Content-Type': "application/json"
            },
            body: JSON.stringify(formObject)
        });

        if (!response.ok) {
            throw new Error(response.message ?? "Unknow Error");
        }

        let result = await  response.json();
        console.log(result);
    } catch (err) {
        console.log(err);
    }

}