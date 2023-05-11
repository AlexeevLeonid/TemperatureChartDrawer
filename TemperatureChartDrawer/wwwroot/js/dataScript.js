"use strict";

async function startup() {
    const responseForSource = await fetch("/source/" + new URLSearchParams(window.location.search).get("id"), {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    const response = await fetch("/record/" + new URLSearchParams(window.location.search).get("id"), {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const records = await response.json();
        const source = await responseForSource.json();
        const data = records.map(function (record) {
            return record.value;
        })

        const labs = records.map(function (record) {
            return record.date;
        })

        const ctx = document.getElementById('myChart');
        chart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labs,
                datasets: [{
                    label: source.name,
                    data: data,
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
        chart.canvas.parentNode.style.height = '85vh';
    }
}
startup();