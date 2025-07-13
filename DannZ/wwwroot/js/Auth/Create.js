let form = document.querySelector("form");

form.addEventListener("submit", registerAccount);

async function  registerAccount(event) {
    event.preventDefault();

    let formData = new FormData(form);

    try {

    let response = await fetch("https://localhost:7238/api/Auth/Create", {
        method: "POST",
        body: formData,
        credentials: 'include'
    }
        );

        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message ?? "Unknow error");
        }

        const data = await response.json();
        console.log(data);

    } catch (err) {
        console.log(err);
    }


}