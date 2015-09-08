function pause_jint(device) {
    var stubs = importNamespace("Plexito.JavaScriptLogic.Stubs");
    var xhr = new stubs.XMLHttpRequest();
    playbackAction(device, "pause", xhr);
}

function play_jint(device) {
    var stubs = importNamespace("Plexito.JavaScriptLogic.Stubs");
    var xhr = new stubs.XMLHttpRequest();
    playbackAction(device, "play", xhr);
}

function skipNext_jint(device) {
    var stubs = importNamespace("Plexito.JavaScriptLogic.Stubs");
    var xhr = new stubs.XMLHttpRequest();
    playbackAction(device, "skipNext", xhr);
}

function skipPrevious_jint(device) {
    var stubs = importNamespace("Plexito.JavaScriptLogic.Stubs");
    var xhr = new stubs.XMLHttpRequest();
    playbackAction(device, "skipPrevious", xhr);
}

function GetStatuses_jint(plexServers) {
    var stubs = importNamespace("Plexito.JavaScriptLogic.Stubs");
    var xhr = new stubs.XMLHttpRequest();
    var statusItems = GetStatuses(plexServers, xhr);

    // Ideally, we should return a json instead of an XML, it would be easier to work with in JavaScript
    // However, JInt is very limited and doesn't allow us to use the DOM parser usually included in a browser.
    // Instead, we just return the string representation of the XML without parsing it.
    // In the future, parsing the XML would allow us to have the filtering logic in JavaScript.
    // To do so, leveraging an external DOM parser would be the path to follow.
    // eg. http://xmljs.sourceforge.net/website/download.html

    // var doc = new XMLDoc(statusItems);
    // var json = xmlToJson_jint(doc);
    return statusItems;
}

function GetStatusesJson_jint(plexServers) {
    var xmlItems = [];
    var statusItems = GetStatuses_jint(plexServers);
    for (index = 0; index < statusItems.length; index++) {
        var xmlDoc = new REXMLLite(statusItems[index]);
        xmlItems.push(JSON.stringify(xmlToJson(xmlDoc.rootElement.childElements[0])));
    }

    return xmlItems;
}

function GetStatusJson_jint(device, plexServers) {
    var statusItems = GetStatuses_jint(plexServers);
    for (index = 0; index < statusItems.length; index++) {
        var xmlDoc = new REXMLLite(statusItems[index]);
        var json = xmlToJson(xmlDoc.rootElement.childElements[0]);
        var deviceToQuery = device.ClientIdentifier;
        var playerParent = json.Video;
        if (playerParent == null)
            playerParent = json.Track;
        if (playerParent == null)
            playerParent = json.Photo;        
        if (playerParent.Player['@attributes'].machineIdentifier === deviceToQuery) {           
            return JSON.stringify(json);
        }
    }
    return null;
}