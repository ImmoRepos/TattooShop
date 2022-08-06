document.addEventListener("DOMContentLoaded", onReady);

function onReady() {
    document.removeEventListener("DOMContentLoaded", onReady);

    window.addEventListener('message', function (evt) {
        let item = evt.data;

        if (item.type === 'save') {
            saveTattoos(item.data)
        } else if (item.type === 'clear') {
            clearTattoos();
        } else if (item.type === 'load') {
            loadTattoos();
        }
    });
}

function clearTattoos() {
    saveTattoos(null);
}

function saveTattoos(data) {
    if (data == null) {
        localStorage.removeItem('tattoodata');
    } else {
        localStorage.setItem('tattoodata', data);
    }
}

function loadTattoos() {
    let data = localStorage.getItem('tattoodata');
    fetch(`https://${GetParentResourceName()}/postTattooData`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json; charset=UTF-8',
        },
        body: JSON.stringify({
            data: data
        })
    });
}