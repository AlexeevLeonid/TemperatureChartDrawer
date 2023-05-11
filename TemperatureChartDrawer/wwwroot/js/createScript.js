"use strict";

function startup() {
    document.getElementById("send").addEventListener("click", async () => {
        const name = document.getElementById("name").value;
        const interval = document.getElementById("interval").value;
        const url = document.getElementById("url").value;
        const left = document.getElementById("left").value;
        const right = document.getElementById("right").value;
        if (isEmpty(name) || isEmpty(interval) || isEmpty(url) || isEmpty(left) || isEmpty(right))
            alert("Одно из полей пустое")
        else {
            await postSource(name, interval, url, left, right);
        }
    });
}

async function postSource(iname, iinterval, iurl, ileft, iright) {
    const response = await fetch("/source", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: iname,
            interval: iinterval,
            url: iurl,
            left: ileft,
            right: iright,
        })
    });
    if (response.status === 200) {
        window.location = "";
    }
    else {
        const error = await response.json();
        alert(error);
    }
}

function isEmpty(str) {
    if (str.trim() == '')
        return true;
    return false;
}

startup();