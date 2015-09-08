function GetDevices_jint(xhr) {
    var stubs = importNamespace("Plexito.JavaScriptLogic.Stubs");
    var xhr = new stubs.XMLHttpRequest();
    var devicesXmlString = GetDevices(xhr);
    var xmlDoc = new REXMLLite(devicesXmlString);
    return JSON.stringify(xmlToJson(xmlDoc.rootElement.childElements[0]));
}