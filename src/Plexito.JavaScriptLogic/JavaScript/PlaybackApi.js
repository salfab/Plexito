function pause(device) {
    var xhr = new XMLHttpRequest();
    playbackAction(device, "pause", xhr);
}

function playbackAction(device, action, xhr) {
    var connectionUri = device.ConnectionUris[0];
    xhr.open("GET", connectionUri + "player/playback/" + action, false);

    xhr.setRequestHeader("Content-Type", "text/xml");
    xhr.send();

    xmlDocument = xhr.responseXML;
}

function GetStatus(device, plexServers) {
    var xhr = new XMLHttpRequest();

    var xmlItems = [];
    var statusItems = GetStatus(device, plexServers, xhr);
    for (index = 0; index < statusItems.length; index++) {
        xmlItems.push(statusItems.documentElement.outerHTML);
    }

    return xmlItems;

    function GetStatusJson(device, plexServers) {
        var xmlItems = [];
        var statusItems = GetStatus(device, plexServers);
        for (index = 0; index < statusItems.length; index++) {
            xmlItems.push(xmlToJson(statusItems));
        }

        return xmlItems;
    }
}

function GetStatus(device, plexServers, xhr) {
    // query all the uris for all servers and keep only the results matching the specified device
    var deviceToQuers = device.Name;
    var xmlItems = [];
    for (index = 0; index < plexServers.length; index++) {
        for (cindex = 0; cindex < plexServers[index].ConnectionUris.length; cindex++) {
            var connectionUri = plexServers[index].ConnectionUris[cindex];
            if (connectionUri != null) {
                xhr.open("GET", connectionUri + "/status/sessions", false);
                xhr.setRequestHeader("Content-Type", "text/xml");
                xhr.send();
                if (xhr.status === 200) {
                    xmlItems.push(xhr.responseXML);
                }
            }
        }
    }
    return xmlItems;
}