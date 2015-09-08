// -------------
/* JSXML XML Tools - REXML Lite
http://jsxml.homestead.com/
Ver 1.2 Jun 18 2001
Copyright 2000 Peter Tracey */

function REXMLLite(XML) {
    this.XML = XML;

    this.rootElement = null;

    this.parse = _p;
    if (this.XML && this.XML != "") this.parse();
}

function _p() {
    var rT = new RegExp("<([^>/ ]*)([^>]*)>", "g");
    var rTT = new RegExp("<([^>/ ]*)([^>]*)>([^<]*)", "g");
    var t = "";
    var txt = "";
    var a = "";
    var e = 0;
    var eL = null;
    if (this.XML.length == 0) return;
    var aEU = this.XML.match(rT);
    var aEUT = this.XML.match(rTT);
    for (var i = 0; i < aEU.length; i++) {
        t = aEU[i].replace(rT, "$1");
        a = aEU[i].replace(rT, "$2");
        txt = aEUT[i].replace(rTT, "$3").replace(/[\r\n\t ]+/g, " ");
        if (aEU[i].substring(1, 2) != "/") {
            if (e == 0) {
                eL = this.rootElement = new _XMLElement(t, a, null, txt);
                e++;
            } else if (aEU[i].substring(aEU[i].length - 2, aEU[i].length - 1) != "/") {
                eL = eL.childElements[eL.childElements.length] = new _XMLElement(t, a, eL, txt);
                e++;
            } else eL.childElements[eL.childElements.length] = new _XMLElement(t, a, eL, txt);
        } else {
            eL = eL.parentElement;
            e--;
            if (eL) eL.text += txt;
        }
    }
}

function _XMLElement(n, a, p, t) {
    this.type = "element";
    this.name = n;
    this.aStr = a;
    this.attributes = null;
    this.childElements = new Array();
    this.parentElement = p;
    this.text = t;

    this.childElement = _c;
    this.attribute = _a;
}

function _c(n) {
    for (var i = 0; i < this.childElements.length; i++) if (this.childElements[i].name == n) return this.childElements[i];
    return null;
}

function _a(n) {
    if (!this.attributes) {
        var ra = new RegExp(" ([^= ]*)=", "g");
        if (this.aStr.match(ra) && this.aStr.match(ra).length) {
            var as = this.aStr.match(ra);
            if (!as.length) as = null;
            else for (var j = 0; j < as.length; j++) {
                as[j] = new Array(
                    (as[j] + "").replace(/[= ]/g, ""),
                    PA(this.aStr, (as[j] + "").replace(/[= ]/g, ""))
                                );
            }
            this.attributes = as;
        }
    }
    if (this.attributes) for (var i = 0; i < this.attributes.length; i++) if (this.attributes[i][0] == n) return this.attributes[i][1];
    return "";
}

function PA(s, n) {
    var s = s + ">";
    if (s.indexOf(n + "='") > -1) var a = new RegExp(".*" + n + "='([^']*)'.*>");
    else if (s.indexOf(n + '="') > -1) var a = new RegExp(".*" + n + '="([^"]*)".*>');
    return s.replace(a, "$1");
}

function xmlToJson(xml) {
    // Create the return object
    var obj = {};

    // do attributes
    // trick to parse all attributes. attributes are not exposed before you try to get an attribute.
    xml.attribute("");
    obj["@attributes"] = {};
    for (var j = 0; j < xml.attributes.length; j++) {
        var attribute = xml.attributes[j];
        obj["@attributes"][attribute[0]] = attribute[1];
    }

    // do children
    for (var i = 0; i < xml.childElements.length; i++) {
        //return nodeName.Type;
        var item = xml.childElements[i];

        var nodeName = item.name;
        if (typeof (obj[nodeName]) == "undefined") {
            obj[nodeName] = xmlToJson(item);
        } else {
            if (typeof (obj[nodeName].push) == "undefined") {
                var old = obj[nodeName];
                obj[nodeName] = [];
                obj[nodeName].push(old);
            }
            obj[nodeName].push(xmlToJson(item));
        }
    }
    return obj;
};