"use strict";

function row(source) {

            const tr = document.createElement("tr");
            tr.setAttribute("data-rowid", source.id);

            const nameTd = document.createElement("td");
            nameTd.append(source.name);
            tr.append(nameTd);

            const surnameTd = document.createElement("td");
            surnameTd.append(source.interval);
            tr.append(surnameTd);

            const sexTd = document.createElement("td");
            sexTd.append(source.url);
            tr.append(sexTd);

            const dateTd = document.createElement("td");
            dateTd.append(source.left);
            tr.append(dateTd);

            const teamTd = document.createElement("td");
            teamTd.append(source.right);
            tr.append(teamTd);

            const dataTd = document.createElement("td");

            const dataLink = document.createElement("button");
            dataLink.append("View chart");
            dataLink.addEventListener("click",
                () => window.location = "/data/?id=" + source.id
            )
            dataTd.append(dataLink);
            tr.appendChild(dataTd);

            const deleteTd = document.createElement("td");

            const deleteLink = document.createElement("button");
            deleteLink.append("Delete");
            deleteLink.addEventListener("click",
                () => deleteSource(source.id)
            )
            deleteTd.append(deleteLink);
            tr.appendChild(deleteTd);

            return tr;
        }

        async function startup() {
            const response = await fetch("/source", {
                method: "GET",
                headers: { "Accept": "application/json" }
            });
            if (response.ok === true) {
                const sources = await response.json();
                const rows = document.getElementById("table");
                sources.forEach(source => rows.append(row(source)));
            }
        }

        async function deleteSource(id) {
            await fetch(
                "/source/" + id, {
                method: "DELETE",
                headers: { "Accept": "application/json" }
            })
        }

        startup()