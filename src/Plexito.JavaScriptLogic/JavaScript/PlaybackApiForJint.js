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

function GetStatus_jint(device, plexServers) {
    var stubs = importNamespace("Plexito.JavaScriptLogic.Stubs");
    var xhr = new stubs.XMLHttpRequest();
    var statusItems = GetStatus(device, plexServers, xhr);

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

function GetStatusJson_jint(device, plexServers) {
    var xmlItems = [];
    var statusItems = GetStatus_jint(device, plexServers);
    for (index = 0; index < statusItems.length; index++) {
        var xmlDoc = new REXMLLite(statusItems[index]);
        xmlItems.push(JSON.stringify(xmlToJson(xmlDoc.rootElement.childElements[0])));
    }

    return xmlItems;
}