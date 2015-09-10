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

function GetStatuses(plexServers) {
    var xhr = new XMLHttpRequest();

    var xmlItems = [];
    var statusItems = GetStatuses(plexServers, xhr);
    for (index = 0; index < statusItems.length; index++) {
        xmlItems.push(statusItems.documentElement.outerHTML);
    }

    return xmlItems;

    function GetStatusesJson(plexServers) {
        var xmlItems = [];
        var statusItems = GetStatuses(plexServers);
        for (index = 0; index < statusItems.length; index++) {
            xmlItems.push(xmlToJson(statusItems));
        }

        return xmlItems;
    }
}

function GetStatuses(plexServers, xhr) {
    // query all the uris for all servers and keep only the results matching the specified device
    var xmlItems = [];
    for (index = 0; index < plexServers.length; index++) {
        for (cindex = 0; cindex < plexServers[index].ConnectionUris.length; cindex++) {
            var connectionUri = plexServers[index].ConnectionUris[cindex];
            if (connectionUri != null) {
                try {
                    xhr.open("GET", connectionUri + "/status/sessions", false);
                    xhr.setRequestHeader("Content-Type", "text/xml");
                    xhr.send();
                    if (xhr.status === 200) {
                        xmlItems.push(xhr.responseXML);
                    }
                }
                catch (e) {
                    // the call didn't go through. maybe a malformed connection URI such as http://:0/status/sessions
                }
            }
        }
    }
    return xmlItems;
}