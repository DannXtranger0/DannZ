export async function getJson(url, options = {}) {
    try {
        const response = await fetch(url, options);
        if (!response.ok)
            throw new Error(response.status);

        return await response.json();

    } catch (err) {
        console.log(err);
    }
}