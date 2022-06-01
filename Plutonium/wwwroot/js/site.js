// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let connection = new signalR.HubConnectionBuilder().withUrl('/hubs/processHub').build();
connection.start().then(() => connection.stream('StreamProcesses').subscribe({
    next: (processes) => {
        $('#processContainer').html('');
        for (var process in processes) {
            var d = '<div>' + processes[process].processName + ' (' + processes[process].processCount + ')<a class="btn btn-danger" style="float:right"  href = "/Home/KillProcess?processName=' + processes[process].processName + '"  id="a-' + processes[process].processName + '"/ >Kill Process<a/></div><br />';
            $("#processContainer").append(d);

              
         }
        document.querySelector(".lastUpdatedDate").innerHTML = processes[0].lastUpdatedDate;
    },
    error: (err) => console.error(err),
    complete: () => { }
})).catch((err) => console.error(err));