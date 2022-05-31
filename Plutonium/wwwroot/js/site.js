// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let connection = new signalR.HubConnectionBuilder().withUrl('/hubs/processHub').build();
connection.start().then(() => connection.stream('StreamProcesses').subscribe({
    next: (process) => {
        document.querySelector(".processName").innerHTML = process.processName;
        document.querySelector(".processCount").innerHTML = process.processCount;
        document.querySelector(".lastUpdatedDate").innerHTML = process.lastUpdatedDate;
    },
    error: (err) => console.error(err),
    complete: () => { }
})).catch((err) => console.error(err));