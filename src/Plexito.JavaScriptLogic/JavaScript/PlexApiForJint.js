function GetDevices_jint(username, password) {
    var stubs = importNamespace("Plexito.JavaScriptLogic.Stubs");
    var xhr = new stubs.XMLHttpRequest();
    var devicesXmlString = GetDevices(username, password, xhr);
    var xmlDoc = new REXMLLite(devicesXmlString);
    return JSON.stringify(xmlToJson(xmlDoc.rootElement.childElements[0]));
}