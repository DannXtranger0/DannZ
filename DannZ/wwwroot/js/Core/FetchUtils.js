export async function getJson(url, options = {}) {
    try {
        const response = await fetch(url, options);
        if (!response.ok)
            throw new Error(response.message);
        let data = await response.json();
        console.log(data);
        return data;

    } catch (err) {
        console.log(err);
    }
}