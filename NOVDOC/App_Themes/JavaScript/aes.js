window.System = { __namespace: true, __typeName: "Sys", getName: function () { return "Sys"; }, __upperCaseTypes: {} }; System.__rootNamespaces = [System]; System.__registeredTypes = {}; System.__registerNameSpace = function (namespacePath) {
    if (typeof (Type) != "undefined" && typeof (Type.registerNamespace) == "function") { Type.registerNamespace(namespacePath); return; }
    var rootObject = window; var namespaceParts = namespacePath.split('.'); for (var i = 0; i < namespaceParts.length; i++) {
        var currentPart = namespaceParts[i]; var ns = rootObject[currentPart]; if (!ns) {
            ns = rootObject[currentPart] = { __namespace: true, __typeName: namespaceParts.slice(0, i + 1).join('.') }; if (i === 0) { System.__rootNamespaces[System.__rootNamespaces.length] = ns; }
            var parsedName; try { parsedName = eval(ns.__typeName); }
            catch (e) { parsedName = null; }
            ns.getName = function ns$getName() { return this.__typeName; }
        }
        rootObject = ns;
    }
}
System.Type = function () { }
System.Type = function (typeName) {
    this.Name = new String('name'); this.Namespace = new String; this.FullName = new String; this.GetType = function () { }
    this.ToSting = function () { return this.FullName; }
    function initialize() { var tn = new String(); tn = arguments[0]; this.FullName = tn; var ta = new Array; if (tn) { ta = tn.split('.'); this.Name = ta[ta.length - 1]; this.Namespace = ta.slice(0, ta.length - 2).join('.'); } }
    initialize.apply(this, arguments);
}
System.Type.GetType = function (typeName) { var type = new System.Type(typeName); return type; }
System.Type.RegisterNamespace = System.__registerNameSpace; System.TypeCode = { Empty: 0, Object: 1, DBNull: 2, Boolean: 3, Char: 4, SByte: 5, Byte: 6, Int16: 7, UInt16: 8, Int32: 9, UInt32: 10, Int64: 11, UInt64: 12, Single: 13, Double: 14, Decimal: 15, DateTime: 16, String: 18 }
System.Extensions = function () { this.Apply = function () { var isServerSide = false; if (typeof (Response) == "object") isServerSide = true; Array.prototype.Clone = function () { var buffer = this.slice(0, this.length); for (var i = 0; i < this.length; i++) buffer[i] = this[i]; return buffer; } } }
System.Extensions = new System.Extensions(); System.Type.Class = System.Type.Class ? System.Type.Class : {}; System.Type.Class.Root = this; System.Type.Class.Inherit = function () {
    Trace.Write("exec System.Class.Inherit(arguments){", 1); this.Classes = new Array(); this.Objects = new Array(); for (var i = 0; i < arguments.length; i++) { arguments[i].prototype.NoInit = true; this.Objects.push(new arguments[i]); arguments[i].prototype.NoInit = false; this.Classes.push(arguments[i]); }
    for (var i = 0; i < this.Objects.length; i++) { if (i == 0) { Trace.Write("Inherit: '" + this.Objects[i].Type + "' Class From: ", 1); } else { Trace.Write(this.Objects[i].Type); } }
    Trace.Write("Done", -2); var finClass = this.Classes[0]
    var finObject = this.Objects[0]; for (var cid = this.Classes.length - 1; cid > 0; cid--) { var srcClass = this.Classes[cid]; var srcObject = this.Objects[cid]; var dstObject = this.Objects[cid - 1]; var dstClass = this.Classes[cid - 1]; Trace.Write("// Inherit: '" + dstObject.Type + "' From: '" + srcObject.Type + "'"); finClass.prototype = srcObject; Trace.Write("1. Import Class Properties: " + finObject.Type + ".prototype <- " + srcObject.Type, 1); Trace.Write("End Import", -2); Trace.Write("2. Fix Prototype Constructor", 1); finClass.prototype.constructor = finClass; Trace.Write("End Fix", -2); Trace.Write("3. Allow to call methods in a superclass", 1); Trace.Write("Import Superclass Properties: " + finObject.Type + ".superclass <- " + srcObject.Type + ".prototype"); finClass.superclass = srcClass.prototype; Trace.Write("End Import", -2); }
    Trace.Write("} //System.Class.Inherit(arrguments)", -2);
}
System.Type.Class.Inherit = function (classTo, classFrom) { classTo.prototype = new classFrom(); classTo.prototype.constructor = classTo; classTo.superclass = classFrom.prototype; }
System.Type.Class.Register = function (typeName, baseType) {
    var classType = eval(typeName); System.Type.Class._registerClass.apply(classType, arguments); classType.IsAbstract = true; classType.prototype.GetType = function () { return eval(typeName) }; classType.prototype.RiseEvent = function (e) {
        if (typeof (e) == "string") { e = new System.EventArgs(e); }
        if (this[e.Name]) this[e.Name](this, e);
    }
}
System.Type.Class.Exists = function (path) {
    var rootObject; if (typeof (Response) == "object") { rootObject = System.Class.Root; } else { rootObject = System.Class.Root; }
    var exists = true; var parts = path.split('.'); for (var i = 0; i < parts.length; i++) {
        var part = parts[i]; if (!rootObject[part]) { exists = false; break; }
        rootObject = rootObject[part];
    }
    return exists;
}
System.Type.Class._registerClass = function (typeName, baseType, interfaceType) {
    this.FullName = typeName; var typeArray = typeName.split("."); this.Name = typeArray[typeArray.length - 1]; if (baseType) { this.BaseType = baseType; System.Class.Inherit(this, baseType); }
    if (interfaceType) { this._interfaces = []; for (var i = 2; i < arguments.length; i++) { interfaceType = arguments[i]; this._interfaces.push(interfaceType); } }
}
System.Class = System.Class ? System.Class : {}; System.Class.Inherit = System.Type.Class.Inherit; System.Class.RegisterClass = System.Type.Class.Register
System.Class.Root = this; System.Parse = function (object, value) {
    var results = null; switch (typeof (object)) {
        case "boolean": results = System.Bool.Parse(value); break; case "number": results = parseFloat(value); break; case "string": results = value; break; case "object": results = value; if (typeof (object["getDate"]) == "function") { results = new Date().GetFromString(value); }
        default: results = value; break;
    }
    return results;
}
System.Bool = {}; System.Bool.Parse = function (value) {
    var results = new String(value).toLowerCase(); if (results == "true" || results == "1" || results == "-1" || results == "on" || results == "yes") { results = true; } else { results = false; }
    return results;
}
System.Bool.IsBoolean = function (value) { var value = new String(value).toLowerCase(); var results = (value == "true" || value == "false" || value == "1" || value == "0" || value == "-1" || value == "on" || value == "off" || value == "yes" || value == "no"); return results; }
System.Random = function () {
    this.Next = function (maxValue) { }
    this.Next = function (minValue, maxValue) {
        switch (arguments.length) { case 0: maxValue = Math.pow(2, 31); minValue = 0; break; case 1: maxValue = arguments[0]; minValue = 0; break; case 2: break; default: return 0; break; }
        var number = minValue; if (maxValue > minValue) { number = Math.floor(Math.random() * (maxValue - minValue)) + minValue; }
        return number;
    }
    this.NextBytes = function (buffer) {
        var length = buffer.length; for (var i = 0; i < length; i++) { buffer[i] = this.Next(0, 256); }
        return buffer;
    }
    this.InitializeClass = function () { }
    this.InitializeClass.apply(this, arguments);
}
System.Array = function () {
    this.Initialize = function () { }
    this.Initialize.apply(this, arguments);
}
System.Array.Reverse = function (array, index, length) { index = (index) ? index : 0; length = (length) ? length : array.length; var iArray = array.slice(index, index + length).reverse(); for (var i = 0; i < length; i++) array[i + index] = iArray[i]; }
System.Array._Copy1 = function (sourceArray, destinationArray, length) { for (var i = 0; i < length; i++) { destinationArray[i] = sourceArray[i]; } }
System.Array._Copy2 = function (sourceArray, sourceIndex, destinationArray, destinationIndex, length) { for (var i = 0; i < length; i++) { destinationArray[destinationIndex + i] = sourceArray[sourceIndex + i]; } }
System.Array.Copy = function () { if (arguments.length == 3) System.Array._Copy1.apply(this, arguments); if (arguments.length == 5) System.Array._Copy2.apply(this, arguments); }
System.Array.FillMultiDimensional = function (array, dimensions, value) {
    if (dimensions.length > 0) { for (var x = 0; x < array.length; x++) { var ar = new Array(dimensions[0]); var dims = dimensions.slice(1); System.Array.FillMultiDimensional(ar, dims, value); array[x] = ar; } } else { for (var x = 0; x < array.length; x++) { array[x] = value; } }
    return array;
}
System.Array.GetMultiDimensional = function (dimensions, value) { var array = new Array(dimensions[0]); return System.Array.FillMultiDimensional(array, dimensions.slice(1), value); }
System.Array.Clear = function (array, index, length) { for (var i = 0; i < length; i++) array[i + index] = 0; }
System.Buffer = function () {
    this.Initialize = function () { }
    this.Initialize.apply(this, arguments);
}
System.Buffer.BlockCopy = function (src, srcOffset, dst, dstOffset, count) { for (var i = 0; i < count; i++) { dst[dstOffset + i] = src[srcOffset + i]; } }
System.Byte = function () {
    var dims = new Array(); for (var i = 0; i < arguments.length; i++) { dims.push(arguments[i]); }
    return System.Array.GetMultiDimensional(dims, 0);
}
System.Int32 = function (value) {
    this.Int = 0; this.DefaultFormat = new String; this.ToString = function (format) {
        var converted = new String(); switch (format) {
            case "B": if (this.Int >= 1048576) { converted = (Math.round(this.Int / 1048576 * 10) / 10) + " MB"; } else if (this.Int >= 1024) { converted = (Math.round(this.Int / 1024 * 10) / 10) + " KB"; } else { converted = new String(this.Int); }
                break; case "X2": case "X4": case "X6": case "X8": var hex = this.Int.toString(16); var len = parseInt(format.substr(1)); var pfx = "00000000".substr(0, len); converted = pfx.substr(0, len - hex.length) + hex; break; default: converted = new String(this.Int); break;
        }
        return converted;
    }
    this.InitializeClass = function () { this.Int = parseInt(value); this.DefaultFormat = ""; }
    this.InitializeClass();
}
System.UInt32 = System.Byte; if (typeof (Response) != "object") {
    if (!window.ActiveXObject) {
        if (document.implementation.hasFeature("XPath", "3.0")) {
            XMLDocument.prototype.selectNodes = function (cXPathString, xNode) {
                if (!xNode) { xNode = this; }
                var oNSResolver = this.createNSResolver(this.documentElement)
                var aItems = this.evaluate(cXPathString, xNode, oNSResolver, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null)
                var aResult = []; for (var i = 0; i < aItems.snapshotLength; i++) { aResult[i] = aItems.snapshotItem(i); }
                return aResult;
            }
            Element.prototype.selectNodes = function (cXPathString) { if (this.ownerDocument.selectNodes) { return this.ownerDocument.selectNodes(cXPathString, this); } else { throw "For XML Elements Only"; } }
            XMLDocument.prototype.selectSingleNode = function (cXPathString, xNode) {
                if (!xNode) { xNode = this; }
                var xItems = this.selectNodes(cXPathString, xNode); if (xItems.length > 0) { return xItems[0]; } else { return null; }
            }
            Element.prototype.selectSingleNode = function (cXPathString) {
                if (this.ownerDocument.selectSingleNode) { return this.ownerDocument.selectSingleNode(cXPathString, this); }
                else { throw "For XML Elements Only"; }
            }
        }
        if (typeof (XMLDocument.setProperty) == 'undefined') {
            XMLDocument.prototype.setProperty = function (name, value) {
                if (name == "SelectionNamespaces") {
                    namespaces = {}; var a = value.split(" xmlns:"); for (var i = 1; i < a.length; i++) { var s = a[i].split("="); namespaces[s[0]] = s[1].replace(/\"/g, ""); }
                    this._ns = { lookupNamespaceURI: function (prefix) { return namespaces[prefix] } }
                }
            }
            XMLDocument.prototype._ns = { lookupNamespaceURI: function () { return null } }
        }
    }
}
System.Extensions.Apply.apply(this);
System.Type.RegisterNamespace("System.IO"); System.Type.RegisterNamespace("System.IO.Directory"); System.IO.Directory.CreateDirectory = function (path) {
    var folderInfo = null; var fso = new ActiveXObject("Scripting.FileSystemObject"); var pathPhysical = new String(path); if (pathPhysical.indexOf(":") == -1) { pathPhysical = Server.MapPath(path); }
    var arrPath = new Array; var regex = new RegExp("\\\\", "g"); arrPath = pathPhysical.split(regex); var pathTemp = new String; for (var i = 0; i < arrPath.length; i++) { var folderName = arrPath[i]; if (i > 0) pathTemp += "\\"; pathTemp += folderName; if (i > 0) { if (!fso.FolderExists(pathTemp)) { Trace.Write("Create folder: " + pathTemp); try { fso.CreateFolder(pathTemp); folderInfo = fso.GetFolder(pathTemp); } catch (ex) { } } } }
    return folderInfo;
}
System.IO.MemoryStream = function (buffer) {
    this.Type = "System.IO.MemoryStream"; this.Buffer = new Array(); this.Capacity = 0; this.Length = 0; this.Position = 0; var isServerSide = false; var stream = null; var adTypeBinary = 1, adTypeText = 2, adSaveCreateOverWrite = 2; this.Read = function (buffer, offset, count) {
        if (isServerSide) { } else {
            for (var i = 0; i < count; i++) { buffer[offset + i] = this.Buffer[this.Position + i]; }
            this.Position += count;
        }
    }
    this.ToArray = function () {
        var array = new Array(); if (isServerSide) { } else { array = this.Buffer.slice(0, this.Buffer.length); }
        return array;
    }
    this.Flush = function () { if (isServerSide) { } else { } }
    this.Write = function (buffer, offset, count) {
        if (isServerSide) { } else {
            for (var i = 0; i < count; i++) { this.Buffer[this.Position + i] = buffer[offset + i]; }
            this.Position += count;
        }
    }
    this.WriteTo = function (stream) { if (isServerSide) { } else { stream.Write(this.Buffer, 0, this.Buffer.length); } }
    this.Close = function () { if (isServerSide) { stream.Close(); } }
    this.Dispose = function () { delete this.Buffer; delete this.Stream; }
    this.Initialize = function () { if (isServerSide) { stream = Server.CreateObject("ADODB.Stream"); stream.Type = adTypeBinary; stream.Open(); } else { if (arguments[0]) { var buffer = arguments[0]; this.Write(buffer, 0, buffer.length); } } }
    this.Initialize.apply(this, arguments);
}
System._bitConverter = function () {
    this.IsLittleEndian; var cMask = {}; cMask[System.TypeCode.Int32] = 0x7FFFFFFF; var sBits = {}
    sBits[System.TypeCode.Int32] = 32; this.GetBytes = function (value, typeCode) {
        switch (typeof (value)) {
            case "boolean": return (value) ? [1] : [0]; break; case "number": switch (typeCode) { case System.TypeCode.Single: return this.GetBytesFromNumber(value, 32); break; case System.TypeCode.Double: return this.GetBytesFromNumber(value, 64); break; case System.TypeCode.Int16: case System.TypeCode.UInt16: return this.GetBytesFromInt16Le(value); break; case System.TypeCode.Int32: case System.TypeCode.UInt32: return this.GetBytesFromInt32Le(value); break; default: return this.GetBytesFromInt32Le(value); break; }
                break; case "object": switch (typeCode) { case System.TypeCode.Single: return this.GetBytesFromNumber(value, 32); break; case System.TypeCode.Double: return this.GetBytesFromNumber(value, 64); break; case System.TypeCode.Int16: case System.TypeCode.UInt16: case System.TypeCode.Int32: case System.TypeCode.UInt32: return this.GetBytesFromInt32ArrayLe(value); break; default: return this.GetBytesFromInt32ArrayLe(value); break; }
                break; default: break;
        }
    }
    this.ToString = function (bytes, separator, format) {
        var sb = new Array(); var s = new String; if (!bytes) return; format = (format) ? format : "X2"; var len = parseInt(format.substr(1)); var pfx = ""; for (var i = 0; i < len; i++) pfx += "0"; for (var i = 0; i < bytes.length; i++) { var b = bytes[i] & 0xFF; var hex = b.toString(16).toUpperCase(); sb.push(pfx.substr(0, len - hex.length) + hex); }
        var sep = (typeof (separator) == "undefined") ? '-' : separator; return sb.join(sep);
    }
    this.ToInt32Le = function (value, startIndex) { return this.ToInt32ArrayLe(value, startIndex, 4)[0]; }
    this.ToInt32Be = function (value, startIndex) { return this.ToInt32ArrayBe(value, startIndex, 4)[0]; }
    this.ToInt32 = this.ToInt32Le; this.ToUInt32Le = function (value, startIndex) { return this.GetUnsigned(this.ToInt32Le(value, startIndex), System.TypeCode.Int32); }
    this.ToUInt32Be = function (value, startIndex) { return this.GetUnsigned(this.ToInt32Be(value, startIndex), System.TypeCode.Int32); }
    this.ToUInt32 = this.ToUInt32Le; function _ToIntArray(bytes, startIndex, count, typeCode, bigEndian) {
        var sizeBits = sBits[typeCode]; var sizeBytes = ((sizeBits) ? sizeBits : 32) / 8; startIndex = (startIndex) ? startIndex : 0; count = (count) ? count : bytes.length - startIndex; var mask = (1 << 8) - 1; var array = Array(); var v, m; for (var i = 0; i < count; i++) { var bi = (i - i % sizeBytes) / sizeBytes; v = bytes[startIndex + i] & mask; m = ((i % sizeBytes) * 8); if (bigEndian) m = sizeBits - 8 - m; array[bi] |= v << m; }
        return array;
    }
    this.ToInt32ArrayLe = function (value, startIndex, count) { return _ToIntArray(value, startIndex, count, System.TypeCode.Int32, false); }
    this._GetBytesFromInt = function (value, typeCode, bigEndian) {
        var sizeBits = sBits[typeCode]; var sizeBytes = ((sizeBits) ? sizeBits : 32) / 8; var bytes = new Array(4); for (b = 0; b < sizeBytes; b++) { m = (bigEndian) ? sizeBytes - 1 - b : b; bytes[m] = (value >> b * 8) & 0xff; }
        return bytes;
    }
    this.GetBytesFromInt32Le = function (value) { return this._GetBytesFromInt(value, System.TypeCode.Int32, false); }
    this.GetUnsigned = function (value, typeCode) {
        var results; var umask = cMask[typeCode + 1]; if (typeof (value) == "number") { results = ((value & umask) << 0) >>> 0; } else { umask = cMask[typeCode + 1]; var length = value.length; results = new Array(length); for (var i = 0; i < length; i++) { var n = value[i]; results[i] = ((n & umask) << 0) >>> 0; } }
        return results;
    }
    this.ToInt32ArrayBe = function (value, startIndex, count) { return _ToIntArray(value, startIndex, count, System.TypeCode.Int32, true); }
}
System.BitConverter = new System._bitConverter();
System.Type.RegisterNamespace("System.Convert"); System.Convert.Base64Array = function () {
    this.S = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/="; this.CA = new Array(); this.IA = new Array(); this.InitializeClass = function () { var c = new String; for (var i = 0; i < this.S.length; i++) { c = this.S.charAt(i); this.CA[i] = c; this.IA[c] = i; } }
    this.InitializeClass();
}
System.Convert.ToBase64String = function (b, wrap) {
    var B64 = new System.Convert.Base64Array(); var bLen = (b) ? b.length : 0; if (bLen == 0) return new Array(0); var eLen = Math.floor(bLen / 3) * 3; var cCnt = ((bLen - 1) / 3 + 1) << 2; var dLen = cCnt + (wrap ? (cCnt - 1) / 76 << 1 : 0); var dArr = new Array(dLen); for (var s = 0, d = 0, cc = 0; s < eLen; ) { var i = (b[s++] & 0xff) << 16 | (b[s++] & 0xff) << 8 | (b[s++] & 0xff); dArr[d++] = B64.CA[(i >>> 18) & 0x3f]; dArr[d++] = B64.CA[(i >>> 12) & 0x3f]; dArr[d++] = B64.CA[(i >>> 6) & 0x3f]; dArr[d++] = B64.CA[i & 0x3f]; if (wrap && ++cc == 19 && d < dLen - 2) { dArr[d++] = '\r'; dArr[d++] = '\n'; cc = 0; } }
    var left = bLen - eLen; if (left > 0) { var j = ((b[eLen] & 0xff) << 10) | (left == 2 ? ((b[bLen - 1] & 0xff) << 2) : 0); dArr[dLen - 4] = B64.CA[j >> 12]; dArr[dLen - 3] = B64.CA[(j >>> 6) & 0x3f]; dArr[dLen - 2] = (left == 2) ? B64.CA[j & 0x3f] : '='; dArr[dLen - 1] = '='; }
    return dArr.join("");
}
System.Convert.FromBase64String = function (s, fix) {
    var B64 = new System.Convert.Base64Array(); if (fix) { var regex = new RegExp("[^" + B64.S + "]", "g"); s = s.replace(regex, ""); }
    var sLen = s.length; if (sLen == 0) return new Array(0); var sIx = 0, eIx = sLen - 1; var pad = s.charAt(eIx) == '=' ? (s.charAt(eIx - 1) == '=' ? 2 : 1) : 0; var cCnt = eIx - sIx + 1; var sepLn = (s.charAt(76) == '\r') ? (cCnt / 78) : 0; var sepCnt = sLen > 76 ? (sepLn << 1) : 0; var len = ((cCnt - sepCnt) * 6 >> 3) - pad; var bytes = new Array(len); var d = 0; var eLen = Math.floor(len / 3) * 3; var i; for (var cc = 0; d < eLen; ) { i = B64.IA[s.charAt(sIx++)] << 18 | B64.IA[s.charAt(sIx++)] << 12 | B64.IA[s.charAt(sIx++)] << 6 | B64.IA[s.charAt(sIx++)]; bytes[d++] = (i >> 16); bytes[d++] = ((i & 0xFFFF) >> 8); bytes[d++] = (i & 0xFF); if (sepCnt > 0 && ++cc == 19) { sIx += 2; cc = 0; } }
    if (d < len) {
        i = 0; for (var j = 0; sIx <= (eIx - pad); j++) { i |= B64.IA[s.charAt(sIx++)] << (18 - j * 6); }
        for (var r = 16; d < len; r -= 8) { var cropBits = Math.pow(2, r + 8) - 1; bytes[d++] = ((i & cropBits) >> r); }
    }
    return bytes;
}
System.Type.RegisterNamespace("System.Text"); System.Type.RegisterNamespace("System.Text.Encoding"); System.Text.Encoding.UTF8Encoder = function () {
    this.Type = "System.Text.Encoding.UTF8Encoder"; var me = this; this.GetBytes = function (s) {
        var bytes = new Array(); var c = new Number; for (var i = 0; i < s.length; i++) { c = s.charCodeAt(i); if (c < 0x80) { bytes.push(c); } else if (c < 0x800) { bytes.push(0xC0 | c >> 6); bytes.push(0x80 | c & 0x3F); } else if (c < 0x10000) { bytes.push(0xE0 | c >> 12); bytes.push(0x80 | c >> 6 & 0x3F); bytes.push(0x80 | c & 0x3F); } else if (c < 0x200000) { bytes.push(0xF0 | c >> 18); bytes.push(0x80 | c >> 12 & 0x3F); bytes.push(0x80 | c >> 6 & 0x3F); bytes.push(0x80 | c & 0x3F); } else { bytes.push(0x3F); } }
        return bytes;
    }
    this.GetString = function (bytes) {
        var s = new String; var b = new Number; var b1 = new Number; var b2 = new Number; var b3 = new Number; var b4 = new Number; var bE = new Number; var ln = bytes.length; for (var i = 0; i < ln; i++) { b = bytes[i]; if (b < 0x80) { s += (b > 0) ? String.fromCharCode(b) : ""; } else if (b < 0xC0) { } else if (b < 0xE0) { if (ln > i + 1) { b1 = (b & 0x1F); i++; b2 = (bytes[i] & 0x3F); bE = (b1 << 6) | b2; s += String.fromCharCode(bE); } } else if (b < 0xF0) { if (ln > i + 2) { b1 = (b & 0xF); i++; b2 = (bytes[i] & 0x3F); i++; b3 = (bytes[i] & 0x3F); bE = (b1 << 12) | (b2 << 6) | b3; s += String.fromCharCode(bE); } } else if (b < 0xF8) { if (ln > i + 3) { b1 = (b & 0x7); i++; b2 = (bytes[i] & 0x3F); i++; b3 = (bytes[i] & 0x3F); i++; b4 = (bytes[i] & 0x3F); bE = (b1 << 18) | (b2 << 12)(b3 << 6) | b4; s += String.fromCharCode(bE); } } else { s += "?" } }
        return s;
    }
    this.InitializeClass = function () { }
    this.InitializeClass();
}
System.Text.Encoding.UTF8 = new System.Text.Encoding.UTF8Encoder(); System.Type.RegisterNamespace("System.Text.Encoding"); System.Text.Encoding.UnicodeEncoder = function () {
    this.Type = "System.Text.Encoding.UnicodeEncoder"; var me = this; this.GetBytes = function (s) {
        var bytes = new Array(); var c = new Number; for (var i = 0; i < s.length; i++) { c = s.charCodeAt(i); if (c > 0xFFFF) { bytes.push(0xDC00 | c & 0x3FF); bytes.push(0xD7C0 + (c >> 10)); } else { bytes.push(c & 0xFF); bytes.push(c >> 8); } }
        return bytes;
    }
    this.GetString = function (bytes) {
        var s = new String; var b = new Number; var b1 = new Number; var b2 = new Number; for (var i = 0; i < bytes.length; i++) { b1 = bytes[i]; i++; b2 = bytes[i]; s += String.fromCharCode((b2 << 8) | b1); }
        return s;
    }
    this.InitializeClass = function () { }
    this.InitializeClass();
}
System.Text.Encoding.Unicode = new System.Text.Encoding.UnicodeEncoder();
/*Cryptography*/
System.Type.RegisterNamespace("System.Security.Cryptography"); System.Security.Cryptography.CryptographicException = function (message) { this.name = "System.Security.Cryptography.CryptographicException"; this.message = message; this.toString = function () { return this.name + ": " + this.message; } }
System.Security.Cryptography.CryptographicException = function (message) { this.name = "System.Security.Cryptography.CryptographicException"; this.message = message; this.toString = function () { return this.name + ": " + this.message; } }
System.Security.Cryptography.Rfc2898DeriveBytes = function (password, salt, iterations) {
    this.IterationCount = 1000; this.Password; this.Salt; this.Hmac; this.HmacLength = 20; var _buffer; var _pos = 0; var _f = 0; this.F = function (s, c, i) {
        var data = new Array(s.length + 4); System.Buffer.BlockCopy(s, 0, data, 0, s.length); for (var b = 0; b < 4; b++) data[s.length + b] = 0; var int4 = System.BitConverter.GetBytes(i); System.Array.Reverse(int4, 0, 4); System.Buffer.BlockCopy(int4, 0, data, s.length, 4); var u1 = this.Hmac.ComputeHash(this.Password, data); data = u1; for (var j = 1; j < c; j++) {
            var un = this.Hmac.ComputeHash(this.Password, data); for (var k = 0; k < this.HmacLength; k++) { u1[k] = (u1[k] ^ un[k]) & 0xff; }
            data = un;
        }
        return u1;
    }
    this.GetBytes = function (cb) {
        var l = Math.floor(cb / this.HmacLength); var r = Math.floor(cb % this.HmacLength); if (r != 0) l++; var result = new Array(cb); var rpos = 0; if (_pos > 0) { var count = Math.min(this.HmacLength - _pos, cb); System.Buffer.BlockCopy(_buffer, _pos, result, 0, count); if (count >= cb) return result; _pos = 0; rpos = (rpos + count) % cb; r = cb - rpos; }
        for (var i = 1; i <= l; i++) { _buffer = this.F(this.Salt, this.IterationCount, ++_f); var count = ((i == l) ? r : this.HmacLength); System.Buffer.BlockCopy(_buffer, _pos, result, rpos, count); var bpos = rpos; rpos = (rpos + _pos + count) % cb; _pos = ((count == this.HmacLength) ? 0 : count); if ((bpos + count) >= cb) return result; }
        return result;
    }
    function Reset() { _buffer = null; _pos = 0; _f = 0; }
    this.Initialize = function () {
        var password = arguments[0]; var salt = arguments[1]; var iterations = arguments[2]; if (typeof (password) == "string") password = System.Text.Encoding.UTF8.GetBytes(password); if (typeof (salt) == "string") salt = System.Text.Encoding.UTF8.GetBytes(salt); this.Password = password; this.Salt = salt
        if (iterations) this.IterationCount = iterations; this.Hmac = new System.Security.Cryptography.HMACSHA1();
    }
    this.Initialize.apply(this, arguments);
}
System.Security.Cryptography.ICryptoTransform = function (algorithm, encryption, rgbIV) {
    var iv = new Array; var algo = null; var encrypt = false; var blockSizeByte = 0; var temp = new Array; var temp2 = new Array; var workBuff = new Array; var workout = new Array; var feedBackByte = 0; var feedBackIter = 0; var m_disposed = false; var lastBlock = false; var _rng; this.InputBlockSize = 0; this.OutputBlockSize = 0; this.CanTransformMultipleBlocks = true; this.CanReuseTransform = false; this._Transform = function (input, output) { switch (algo.Mode) { case System.Security.Cryptography.CipherMode.ECB: ECB(input, output); break; case System.Security.Cryptography.CipherMode.CBC: CBC(input, output); break; case System.Security.Cryptography.CipherMode.CFB: CFB(input, output); break; case System.Security.Cryptography.CipherMode.OFB: OFB(input, output); break; case System.Security.Cryptography.CipherMode.CTS: CTS(input, output); break; default: var msg = "Unkown CipherMode" + algo.Mode; throw msg; } }
    function ECB(input, output) { if (encrypt) { var outputBuffer = algo.Encrypt(algo.Key, input, System.Security.Cryptography.CipherMode.ECB); System.Buffer.BlockCopy(outputBuffer, 0, output, 0, blockSizeByte); } else { var outputBuffer = algo.Decrypt(algo.Key, input, System.Security.Cryptography.CipherMode.ECB); System.Buffer.BlockCopy(outputBuffer, 0, output, 0, blockSizeByte); } }
    function CBC(input, output) { if (encrypt) { for (var i = 0; i < blockSizeByte; i++) temp[i] ^= input[i]; ECB(temp, output); System.Buffer.BlockCopy(output, 0, temp, 0, blockSizeByte); } else { System.Buffer.BlockCopy(input, 0, temp2, 0, blockSizeByte); ECB(input, output); for (var i = 0; i < blockSizeByte; i++) output[i] ^= temp[i]; System.Buffer.BlockCopy(temp2, 0, temp, 0, blockSizeByte); } }
    function CFB(input, output) {
        if (encrypt) {
            for (var x = 0; x < feedBackIter; x++) {
                ECB(temp, temp2); for (var i = 0; i < feedBackByte; i++) { output[i + x] = (temp2[i] ^ input[i + x]); }
                System.Buffer.BlockCopy(temp, feedBackByte, temp, 0, blockSizeByte - feedBackByte); System.Buffer.BlockCopy(output, x, temp, blockSizeByte - feedBackByte, feedBackByte);
            }
        } else { for (var x = 0; x < feedBackIter; x++) { encrypt = true; ECB(temp, temp2); encrypt = false; System.Buffer.BlockCopy(temp, feedBackByte, temp, 0, blockSizeByte - feedBackByte); System.Buffer.BlockCopy(input, x, temp, blockSizeByte - feedBackByte, feedBackByte); for (var i = 0; i < feedBackByte; i++) { output[i + x] = (temp2[i] ^ input[i + x]); } } }
    }
    function OFB(input, utput) { throw "OFB isn't supported"; }
    function CTS(input, output) { throw "CTS  isn't supported"; }
    function CheckInput(inputBuffer, inputOffset, inputCount) { if (!inputBuffer) throw "inputBuffer is can't be null"; if (inputOffset < 0) throw "inputOffset is out of range"; if (inputCount < 0) throw "inputCount is out of range"; if (inputOffset > inputBuffer.length - inputCount) { throw "inputBuffer is out of range (overflow)"; } }
    this.TransformBlock = function (inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset) {
        if (m_disposed)
            throw new System.ObjectDisposedException("Object is disposed."); CheckInput(inputBuffer, inputOffset, inputCount); if (outputBuffer == null)
            throw new System.ArgumentNullException("outputBuffer"); if (outputOffset < 0)
            throw new System.ArgumentOutOfRangeException("outputOffset", "< 0"); if (outputOffset > outputBuffer.length - inputCount)
            throw new System.ArgumentException("outputBuffer", "Overflow"); return this._InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
    }
    function KeepLastBlock() { return ((!encrypt) && (algo.Padding != System.Security.Cryptography.PaddingMode.Zeros) && (algo.Padding != System.Security.Cryptography.PaddingMode.None)); }
    this._InternalTransformBlock = function (inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset) {
        var offs = inputOffset; var full = 0; if (inputCount != blockSizeByte) {
            if ((inputCount % blockSizeByte) != 0) { throw new System.Security.Cryptography.CryptographicException("Invalid input block size."); }
            full = inputCount / blockSizeByte;
        } else { full = 1; }
        if (KeepLastBlock()) full--; var total = 0; if (lastBlock) { this._Transform(workBuff, workout); System.Buffer.BlockCopy(workout, 0, outputBuffer, outputOffset, blockSizeByte); outputOffset += blockSizeByte; total += blockSizeByte; lastBlock = false; }
        for (var i = 0; i < full; i++) { System.Buffer.BlockCopy(inputBuffer, offs, workBuff, 0, blockSizeByte); this._Transform(workBuff, workout); System.Buffer.BlockCopy(workout, 0, outputBuffer, outputOffset, blockSizeByte); offs += blockSizeByte; outputOffset += blockSizeByte; total += blockSizeByte; }
        if (KeepLastBlock()) { System.Buffer.BlockCopy(inputBuffer, offs, workBuff, 0, blockSizeByte); lastBlock = true; }
        return total;
    }
    function Random(buffer, start, length, zeroBytes) {
        if (_rng == null) { _rng = new System.Security.Cryptography.RNGCryptoServiceProvider(); }
        var random = new System.Byte(length); if (zeroBytes) { _rng.GetBytes(random); } else { _rng.GetNonZeroBytes(random); }
        System.Buffer.BlockCopy(random, 0, buffer, start, length);
    }
    function ThrowBadPaddingException(padding, length, position) { var msg = "Bad " + padding + " padding."; if (length >= 0) msg += " Invalid length " + length + "."; if (position >= 0) msg += " Error found at position " + position + "."; throw new System.Security.Cryptography.CryptographicException(msg); }
    this._Padding = function (inputBuffer, inputOffset, inputCount) {
        var rem = blockSizeByte - inputCount; var paddingSize = (rem > 0) ? rem : blockSizeByte; var paddedInput = new System.Byte(paddingSize); var blocksCount = 1; var newBlock = new Array(); switch (algo.Padding) {
            case System.Security.Cryptography.PaddingMode.None: if (rem != 0) { throw new System.Security.Cryptography.CryptographicException("Invalid block length"); }
            case System.Security.Cryptography.PaddingMode.Zeros: for (var i = 0; i < paddedInput.length; i++) { paddedInput[i] = 0; }
                if (rem == 0) blocksCount = 2; break; case System.Security.Cryptography.PaddingMode.ANSIX923: paddedInput[paddedInput.length - 1] = paddingSize; if (rem == 0) blocksCount = 2; break; case System.Security.Cryptography.PaddingMode.ISO10126: Random(paddedInput, 0, paddedInput.length - 1, true); paddedInput[paddedInput.length - 1] = paddingSize; if (rem == 0) blocksCount = 2; break; case System.Security.Cryptography.PaddingMode.PKCS7: for (var i = 0; i < paddedInput.length; i++) { paddedInput[i] = paddingSize; }
                if (rem == 0) blocksCount = 2; break; case System.Security.Cryptography.PaddingMode.RsaEsPkcs: Random(paddedInput, 1, paddedInput.length - 2, false); paddedInput[0] = 0; paddedInput[paddedInput.length - 2] = 2; paddedInput[paddedInput.length - 1] = 0; if (rem == 0) blocksCount = 2; break; case System.Security.Cryptography.PaddingMode.RsaEsOaep: var oaep = new System.Security.Cryptography.PKCS1Padding()
                var mgf = new System.Security.Cryptography.PKCS1MaskGenerationMethod(); var hash = new System.Security.Cryptography.SHA1CryptoServiceProvider(); var rng = new System.Security.Cryptography.RNGCryptoServiceProvider(); newBlock = oaep.RsaEsOaepEncrypt(algo, hash, mgf, rng, inputBuffer); default: break;
        }
        var iBuffer = new System.Byte(blockSizeByte * blocksCount); var oBuffer = new System.Byte(blockSizeByte * blocksCount); if (newBlock.length == 0) { System.Buffer.BlockCopy(inputBuffer, inputOffset, iBuffer, 0, inputCount); if ((rem > 0) || (rem == 0 && blocksCount == 2)) { System.Buffer.BlockCopy(paddedInput, 0, iBuffer, inputCount, paddingSize); } } else { System.Buffer.BlockCopy(newBlock, inputOffset, iBuffer, 0, inputCount + paddingSize); }
        var result = {}; result["blocksCount"] = blocksCount; result["iBuffer"] = iBuffer; result["oBuffer"] = oBuffer; return result;
    }
    function ConvertIntToByteArray(dwInput, counter) { var bytes = System.BitConverter.GetBytesFromInt32Be(dwInput); System.Buffer.BlockCopy(bytes, 0, counter, 0, bytes.length); }
    this._PaddingRemove = function (res, inputOffset, inputCount) {
        var total = res.length; var padding = 0; var newBlock = new Array(); switch (algo.Padding) {
            case System.Security.Cryptography.PaddingMode.ANSIX923: padding = ((total > 0) ? res[total - 1] : 0); if ((padding == 0) || (padding > blockSizeByte)) { System.Security.Cryptography.ThrowBadPaddingException(algo.Padding, padding, -1); }
                for (var i = padding; i > 0; i--) {
                    if (res[total - 1 - i] != 0x00)
                        System.Security.Cryptography.ThrowBadPaddingException(algo.Padding, -1, i);
                }
                total -= padding; break; case System.Security.Cryptography.PaddingMode.ISO10126: padding = ((total > 0) ? res[total - 1] : 0); if ((padding == 0) || (padding > blockSizeByte))
                    System.Security.Cryptography.ThrowBadPaddingException(algo.Padding, padding, -1); total -= padding; break; case System.Security.Cryptography.PaddingMode.PKCS7: padding = ((total > 0) ? res[total - 1] : 0); if ((padding == 0) || (padding > blockSizeByte)) { Trace.Write(padding + ", " + blockSizeByte); System.Security.Cryptography.ThrowBadPaddingException(algo.Padding, padding, -1); }
                for (var i = padding - 1; i > 0; i--)
                { if (res[total - 1 - i] != padding) { System.Security.Cryptography.ThrowBadPaddingException(algo.Padding, -1, i); } }
                total -= padding; break; case System.Security.Cryptography.PaddingMode.RsaEsPkcs: if (res[total - 1] != 0x00)
                    System.Security.Cryptography.ThrowBadPaddingException(algo.Padding, -1, total - 1); if (res[total - 2] != 0x02)
                    System.Security.Cryptography.ThrowBadPaddingException(algo.Padding, -1, total - 2); for (var i = total - 1 - 2; i > 0; i--)
                { if (res[i] == 0x00) { total = i; break; } }
                break; case System.Security.Cryptography.PaddingMode.RsaEsOaep: var oaep = new System.Security.Cryptography.PKCS1Padding()
                var mgf = new System.Security.Cryptography.PKCS1MaskGenerationMethod(); var hash = new System.Security.Cryptography.SHA1CryptoServiceProvider(); newBlock = oaep.RsaEsOaepDecrypt(algo, hash, mgf, res); return newBlock; case System.Security.Cryptography.PaddingMode.None: case System.Security.Cryptography.PaddingMode.Zeros: break;
        }
        if (total > 0) { var data = new System.Byte(total); System.Buffer.BlockCopy(res, 0, data, 0, total); System.Array.Clear(res, 0, res.length); return data; } else { return new System.Byte(0); }
    }
    this._FinalEncrypt = function (inputBuffer, inputOffset, inputCount) {
        var result = this._Padding(inputBuffer, inputOffset, inputCount); var blocksCount = result.blocksCount; var iBuffer = result.iBuffer; var oBuffer = result.oBuffer; for (var i = 0; i < blocksCount; i++) { var offset = i * blockSizeByte; this._InternalTransformBlock(iBuffer, offset, blockSizeByte, oBuffer, offset); }
        return oBuffer;
    }
    this._FinalDecrypt = function (inputBuffer, inputOffset, inputCount) {
        if ((inputCount % blockSizeByte) > 0) { throw new System.Security.Cryptography.CryptographicException("Invalid input block size."); }
        var total = inputCount; if (lastBlock) total += blockSizeByte; var res = new System.Byte(total); var outputOffset = 0; while (inputCount > 0) { var len = this._InternalTransformBlock(inputBuffer, inputOffset, blockSizeByte, res, outputOffset); inputOffset += blockSizeByte; outputOffset += len; inputCount -= blockSizeByte; }
        if (lastBlock) { this._Transform(workBuff, workout); System.Buffer.BlockCopy(workout, 0, res, outputOffset, blockSizeByte); outputOffset += blockSizeByte; lastBlock = false; }
        return this._PaddingRemove(res, inputOffset, inputCount);
    }
    this.TransformFinalBlock = function (inputBuffer, inputOffset, inputCount) { if (m_disposed) throw new ObjectDisposedException("Object is disposed"); CheckInput(inputBuffer, inputOffset, inputCount); if (encrypt) { return this._FinalEncrypt(inputBuffer, inputOffset, inputCount); } else { return this._FinalDecrypt(inputBuffer, inputOffset, inputCount); } }
    this.Initialize = function (algorithm, encryption) {
        algo = algorithm; encrypt = encryption; if (algo) {
            blockSizeByte = (algo.BlockSize >> 3); this.InputBlockSize = blockSizeByte; this.OutputBlockSize = blockSizeByte; temp = new System.Byte(blockSizeByte); System.Buffer.BlockCopy(algo.IV, 0, temp, 0, Math.min(blockSizeByte, algo.IV.length)); temp2 = new System.Byte(blockSizeByte); feedBackByte = (algo.FeedbackSize >> 3); if (feedBackByte != 0)
                feedBackIter = blockSizeByte / feedBackByte; workBuff = new System.Byte(blockSizeByte); workout = new System.Byte(blockSizeByte);
        }
    }
    this.Initialize.apply(this, arguments);
}
System.Security.Cryptography.CryptoStream = function (stream, transform, mode) {
    this.Type = "System.Security.Cryptography.CryptoStream"; var _stream; var _transform; var _mode; var _currentBlock = new Array; var _disposed = false; var _flushedFinalBlock = false; var _partialCount = 0; var _endOfStream = false; var _waitingBlock = new Array; var _waitingCount = 0; var _transformedBlock = new Array; var _transformedPos = 0; var _transformedCount = 0; var _workingBlock = new Array; var _workingCount = 0; this.Read = function (buffer, offset, count) {
        var result = 0; if ((count == 0) || ((_transformedPos == _transformedCount) && (_endOfStream))) { return result; }
        if (_waitingBlock == null) { _transformedBlock = new System.Byte(_transform.OutputBlockSize << 2); _transformedPos = 0; _transformedCount = 0; _waitingBlock = new System.Byte(_transform.InputBlockSize); _waitingCount = _stream.Read(_waitingBlock, 0, _waitingBlock.length); }
        while (count > 0) {
            var length = (_transformedCount - _transformedPos); if (length < _transform.InputBlockSize) {
                var transformed = 0; _workingCount = _stream.Read(_workingBlock, 0, _workingBlock.length); _endOfStream = (_workingCount < _transform.InputBlockSize); if (!_endOfStream) { transformed = _transform.TransformBlock(_waitingBlock, 0, _waitingBlock.length, _transformedBlock, _transformedCount); System.Buffer.BlockCopy(_workingBlock, 0, _waitingBlock, 0, _workingCount); _waitingCount = _workingCount; } else {
                    if (_workingCount > 0) { transformed = _transform.TransformBlock(_waitingBlock, 0, _waitingBlock.length, _transformedBlock, _transformedCount); System.Buffer.BlockCopy(_workingBlock, 0, _waitingBlock, 0, _workingCount); _waitingCount = _workingCount; length += transformed; _transformedCount += transformed; }
                    var input = _transform.TransformFinalBlock(_waitingBlock, 0, _waitingCount); transformed = input.length; System.Buffer.BlockCopy(input, 0, _transformedBlock, _transformedCount, input.length); System.Array.Clear(input, 0, input.length);
                }
                length += transformed; _transformedCount += transformed;
            }
            if (_transformedPos > _transform.InputBlockSize) { System.Buffer.BlockCopy(_transformedBlock, _transformedPos, _transformedBlock, 0, length); _transformedCount -= _transformedPos; _transformedPos = 0; }
            length = ((count < length) ? count : length); if (length > 0) { System.Buffer.BlockCopy(_transformedBlock, _transformedPos, buffer, offset, length); _transformedPos += length; result += length; offset += length; count -= length; }
            if (((length != _transform.InputBlockSize) && (_waitingCount != _transform.InputBlockSize)) || (_endOfStream)) { count = 0; }
        }
        return result;
    }
    this.Write = function (buffer, offset, count) {
        if ((_partialCount > 0) && (_partialCount != _transform.InputBlockSize)) { var remainder = _transform.InputBlockSize - _partialCount; remainder = ((count < remainder) ? count : remainder); System.Buffer.BlockCopy(buffer, offset, _workingBlock, _partialCount, remainder); _partialCount += remainder; offset += remainder; count -= remainder; }
        var bufferPos = offset; while (count > 0) {
            if (_partialCount == _transform.InputBlockSize) { var len = _transform.TransformBlock(_workingBlock, 0, _partialCount, _currentBlock, 0); _stream.Write(_currentBlock, 0, len); _partialCount = 0; }
            if (_transform.CanTransformMultipleBlocks) {
                var numBlock = Math.floor(((_partialCount + count) / _transform.InputBlockSize)); var multiSize = (numBlock * _transform.InputBlockSize); if (numBlock > 0) { var multiBlocks = new System.Byte(multiSize); var len = _transform.TransformBlock(buffer, offset, multiSize, multiBlocks, 0); _stream.Write(multiBlocks, 0, len); _partialCount = count - multiSize; System.Buffer.BlockCopy(buffer, offset + multiSize, _workingBlock, 0, _partialCount); } else { System.Buffer.BlockCopy(buffer, offset, _workingBlock, _partialCount, count); _partialCount += count; }
                count = 0;
            } else { var len = Math.min(_transform.InputBlockSize - _partialCount, count); System.Buffer.BlockCopy(buffer, bufferPos, _workingBlock, _partialCount, len); bufferPos += len; _partialCount += len; count -= len; }
        }
    }
    this.Flush = function () { if (_stream != null) _stream.Flush(); }
    this.FlushFinalBlock = function () {
        _flushedFinalBlock = true; var finalBuffer = _transform.TransformFinalBlock(_workingBlock, 0, _partialCount); if (_stream != null) {
            _stream.Write(finalBuffer, 0, finalBuffer.length); if (_stream.Type == "System.Security.Cryptography.CryptoStream") { _stream.FlushFinalBlock(); }
            _stream.Flush();
        }
        System.Array.Clear(finalBuffer, 0, finalBuffer.length);
    }
    this.ToArray = function () { return stream.ToArray(); }
    this.Close = function () {
        if ((!_flushedFinalBlock) && (_mode == System.Security.Cryptography.CryptoStreamMode.Write)) { this.FlushFinalBlock(); }
        if (_stream != null) _stream.Close();
    }
    this.Dispose = function () {
        if (!_disposed) {
            _disposed = true; if (_workingBlock != null)
                System.Array.Clear(_workingBlock, 0, _workingBlock.length); if (_currentBlock != null)
                System.Array.Clear(_currentBlock, 0, _currentBlock.length); if (disposing) { _stream = null; _workingBlock = null; _currentBlock = null; }
        }
    }
    this.Initialize = function () { _stream = arguments[0]; _transform = arguments[1]; _mode = arguments[2]; _disposed = false; if (_transform) { _workingBlock = new System.Byte(_transform.InputBlockSize); if (_mode == System.Security.Cryptography.CryptoStreamMode.Read) { _currentBlock = new System.Byte(_transform.InputBlockSize); } else if (_mode == System.Security.Cryptography.CryptoStreamMode.Write) { _currentBlock = new System.Byte(_transform.OutputBlockSize); } } }
    this.Initialize.apply(this, arguments);
}
System.Security.Cryptography.HashAlgorithm = function () {
    this.CanReuseTransform = true; this.CanTransformMultipleBlocks = true; this.InputBlockSize = 1; this.OutputBlockSize = 1; this.HashSizeValue = 0; this.HashValue = new System.Byte(); this.State = 0; this.HashSize = this.HashSizeValue; this._ComputeHash1 = function (buffer)
    { return this._ComputeHash2(buffer, 0, buffer.length); }
    this._ComputeHash2 = function (buffer, offset, count)
    { this.HashCore(buffer, offset, count); this.HashValue = this.HashFinal(); var buffer2 = this.Hash(); this.Initialize(); return buffer2; }
    this.ComputeHash = function () {
        if (arguments.length == 1) { var value = arguments[0]; if (typeof (value) == "string") value = System.Text.Encoding.UTF8.GetBytes(value); var args = new Array(0); args[0] = value; return this._ComputeHash1.apply(this, args); }
        if (arguments.length == 3) return this._ComputeHash2.apply(this, arguments);
    }
    this.HashCore = function (array, ibStart, cbSize) { }
    this.HashFinal = function () { }
    this.Initialize = function () { }
    this.TransformBlock = function (inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset) {
        this.State = 1; this.HashCore(inputBuffer, inputOffset, inputCount); if ((outputBuffer != null) && ((inputBuffer != outputBuffer) || (inputOffset != outputOffset)))
        { System.Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount); }
        return inputCount;
    }
    this.TransformFinalBlock = function (inputBuffer, inputOffset, inputCount) {
        this.HashCore(inputBuffer, inputOffset, inputCount); this.HashValue = this.HashFinal(); var dst = new System.Byte(inputCount); if (inputCount != 0)
        { System.Buffer.BlockCopy(inputBuffer, inputOffset, dst, 0, inputCount); }
        this.State = 0; return dst;
    }
    this.Hash = function () { return this.HashValue.Clone(); }
}
System.Security.Cryptography.RNGCryptoServiceProvider = function () {
    var rnd; this.GetBytes = function (data) { var length = data.length; for (var i = 0; i < length; i++) { data[i] = rnd.Next(0, 256); } }
    this.GetNonZeroBytes = function (data) { var length = data.length; for (var i = 0; i < length; i++) { data[i] = rnd.Next(1, 256); } }
    this.Dispose = function () { m_disposed = true; }
    this.Initialize = function () { rnd = new System.Random(); }
    this.Initialize.apply(this, arguments);
}
System.Security.Cryptography.CipherMode = { CBC: 1, ECB: 2, OFB: 3, CFB: 4, CTS: 5 }
System.Security.Cryptography.PaddingMode = { None: 1, PKCS7: 2, Zeros: 3, ANSIX923: 4, ISO10126: 5, RsaEsPkcs: 6, RsaEsOaep: 7 }
System.Security.Cryptography.CryptoStreamMode = { Read: 0, Write: 1 }
System.Type.RegisterNamespace("System.Security.Cryptography"); System.Security.Cryptography.HMACSHA1 = function (key) {
    this.Type = "System.Security.Cryptography.HMACSHA1"; this.Name = "HMACSHA1"; this.Algorithm; this.Key; this.HashSize = 160; this.HashName = "SHA1"; this.ComputeHash = function (key, data) {
        if (!data) { data = key; key = this.Key; }
        if (typeof (key) == "string") key = System.Text.Encoding.UTF8.GetBytes(key); if (typeof (data) == "string") data = System.Text.Encoding.UTF8.GetBytes(data); return this._ComputeHash(key, data);
    }
    this.ComputeHashAsHex = function (key, data) { var bytes = this.ComputeHash(key, data); return System.BitConverter.ToString(bytes, ''); }
    this.ComputeHashAsBase64 = function (key, data) { var bytes = this.ComputeHash(key, data); return System.Convert.ToBase64String(bytes, false); }
    this._ComputeHash = function (key, data) {
        if (!data) { data = key; key = this.Key; }
        if (key.length > 64) key = this.Algorithm.ComputeHash(key); var ipad = new Array(64), opad = new Array(64); for (var i = 0; i < 64; i++) { ipad[i] = key[i] ^ 0x36; opad[i] = key[i] ^ 0x5C; }
        var hash = this.Algorithm.ComputeHash(ipad.concat(data)); return this.Algorithm.ComputeHash(opad.concat(hash));
    }
    this.Initialize = function () { this.Algorithm = new System.Security.Cryptography.SHA1(); this.Key = arguments[0]; }
    this.Initialize.apply(this, arguments);
}
System.Type.RegisterNamespace("System.Security.Cryptography"); System.Security.Cryptography.RijndaelManaged = function () {
    this.KeySize = 256; this.BlockSize = 128; this.FeedbackSize = 128; this.IV; this.Key; this.Mode = System.Security.Cryptography.CipherMode.CBC; this.Padding = System.Security.Cryptography.PaddingMode.PKCS7; var rng; var Rcon = [0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91]; var S5 = new Array(256); var T1 = new Array(256); var T2 = new Array(256); var T3 = new Array(256); var T4 = new Array(256); var T5 = new Array(256); var T6 = new Array(256); var T7 = new Array(256); var T8 = new Array(256); var U1 = new Array(256); var U2 = new Array(256); var U3 = new Array(256); var U4 = new Array(256); var S = [0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76, 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0, 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, 0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75, 0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84, 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8, 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2, 0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73, 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb, 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, 0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08, 0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, 0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e, 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf, 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16]; function B0(x) { return (x & 255); }
    function B1(x) { return ((x >> 8) & 255); }
    function B2(x) { return ((x >> 16) & 255); }
    function B3(x) { return ((x >> 24) & 255); }
    function F1(x0, x1, x2, x3) { return B1(T1[x0 & 255]) | (B1(T1[(x1 >> 8) & 255]) << 8) | (B1(T1[(x2 >> 16) & 255]) << 16) | (B1(T1[x3 >>> 24]) << 24); }
    function packBytes(octets) {
        var i, j; var len = octets.length; var b = new Array(len / 4); if (!octets || len % 4) return; for (i = 0, j = 0; j < len; j += 4) { b[i++] = octets[j] | (octets[j + 1] << 8) | (octets[j + 2] << 16) | (octets[j + 3] << 24); }
        return b;
    }
    function unpackBytes(packed) {
        var j; var i = 0, l = packed.length; var r = new Array(l * 4); for (j = 0; j < l; j++) { r[i++] = B0(packed[j]); r[i++] = B1(packed[j]); r[i++] = B2(packed[j]); r[i++] = B3(packed[j]); }
        return r;
    }
    var maxkc = 8; var maxrk = 14; function keyExpansion(key) {
        var kc, i, j, r, t; var rounds; var keySched = new Array(maxrk + 1); var keylen = key.length; var k = new Array(maxkc); var tk = new Array(maxkc); var rconpointer = 0; if (keylen == 16) { rounds = 10; kc = 4; } else if (keylen == 24) { rounds = 12; kc = 6 } else if (keylen == 32) { rounds = 14; kc = 8 } else { return; }
        for (i = 0; i < maxrk + 1; i++) keySched[i] = new Array(4); for (i = 0, j = 0; j < keylen; j++, i += 4) { k[j] = key[i] | (key[i + 1] << 8) | (key[i + 2] << 16) | (key[i + 3] << 24); }
        for (j = kc - 1; j >= 0; j--) tk[j] = k[j]; r = 0; t = 0; for (j = 0; (j < kc) && (r < rounds + 1); ) {
            for (; (j < kc) && (t < 4); j++, t++) { keySched[r][t] = tk[j]; }
            if (t == 4) { r++; t = 0; }
        }
        while (r < rounds + 1) {
            var temp = tk[kc - 1]; tk[0] ^= S[B1(temp)] | (S[B2(temp)] << 8) | (S[B3(temp)] << 16) | (S[B0(temp)] << 24); tk[0] ^= Rcon[rconpointer++]; if (kc != 8) { for (j = 1; j < kc; j++) tk[j] ^= tk[j - 1]; } else { for (j = 1; j < kc / 2; j++) tk[j] ^= tk[j - 1]; temp = tk[kc / 2 - 1]; tk[kc / 2] ^= S[B0(temp)] | (S[B1(temp)] << 8) | (S[B2(temp)] << 16) | (S[B3(temp)] << 24); for (j = kc / 2 + 1; j < kc; j++) tk[j] ^= tk[j - 1]; }
            for (j = 0; (j < kc) && (r < rounds + 1); ) {
                for (; (j < kc) && (t < 4); j++, t++) { keySched[r][t] = tk[j]; }
                if (t == 4) { r++; t = 0; }
            }
        }
        this.rounds = rounds; this.rk = keySched; return this;
    }
    function AESencrypt(block, ctx) {
        var r; var t0, t1, t2, t3; var b = packBytes(block); var rounds = ctx.rounds; var b0 = b[0]; var b1 = b[1]; var b2 = b[2]; var b3 = b[3]; for (r = 0; r < rounds - 1; r++) { t0 = b0 ^ ctx.rk[r][0]; t1 = b1 ^ ctx.rk[r][1]; t2 = b2 ^ ctx.rk[r][2]; t3 = b3 ^ ctx.rk[r][3]; b0 = T1[t0 & 255] ^ T2[(t1 >> 8) & 255] ^ T3[(t2 >> 16) & 255] ^ T4[t3 >>> 24]; b1 = T1[t1 & 255] ^ T2[(t2 >> 8) & 255] ^ T3[(t3 >> 16) & 255] ^ T4[t0 >>> 24]; b2 = T1[t2 & 255] ^ T2[(t3 >> 8) & 255] ^ T3[(t0 >> 16) & 255] ^ T4[t1 >>> 24]; b3 = T1[t3 & 255] ^ T2[(t0 >> 8) & 255] ^ T3[(t1 >> 16) & 255] ^ T4[t2 >>> 24]; }
        r = rounds - 1; t0 = b0 ^ ctx.rk[r][0]; t1 = b1 ^ ctx.rk[r][1]; t2 = b2 ^ ctx.rk[r][2]; t3 = b3 ^ ctx.rk[r][3]; b[0] = F1(t0, t1, t2, t3) ^ ctx.rk[rounds][0]; b[1] = F1(t1, t2, t3, t0) ^ ctx.rk[rounds][1]; b[2] = F1(t2, t3, t0, t1) ^ ctx.rk[rounds][2]; b[3] = F1(t3, t0, t1, t2) ^ ctx.rk[rounds][3]; return unpackBytes(b);
    }
    function prepare_decryption(key) {
        var r, w; var rk2 = new Array(maxrk + 1); var ctx = new keyExpansion(key); var rounds = ctx.rounds; for (r = 0; r < maxrk + 1; r++) { rk2[r] = new Array(4); rk2[r][0] = ctx.rk[r][0]; rk2[r][1] = ctx.rk[r][1]; rk2[r][2] = ctx.rk[r][2]; rk2[r][3] = ctx.rk[r][3]; }
        for (r = 1; r < rounds; r++) { w = rk2[r][0]; rk2[r][0] = U1[B0(w)] ^ U2[B1(w)] ^ U3[B2(w)] ^ U4[B3(w)]; w = rk2[r][1]; rk2[r][1] = U1[B0(w)] ^ U2[B1(w)] ^ U3[B2(w)] ^ U4[B3(w)]; w = rk2[r][2]; rk2[r][2] = U1[B0(w)] ^ U2[B1(w)] ^ U3[B2(w)] ^ U4[B3(w)]; w = rk2[r][3]; rk2[r][3] = U1[B0(w)] ^ U2[B1(w)] ^ U3[B2(w)] ^ U4[B3(w)]; }
        this.rk = rk2; this.rounds = rounds; return this;
    }
    function AESdecrypt(block, ctx) {
        var r; var t0, t1, t2, t3; var rounds = ctx.rounds; var b = packBytes(block); for (r = rounds; r > 1; r--) { t0 = b[0] ^ ctx.rk[r][0]; t1 = b[1] ^ ctx.rk[r][1]; t2 = b[2] ^ ctx.rk[r][2]; t3 = b[3] ^ ctx.rk[r][3]; b[0] = T5[B0(t0)] ^ T6[B1(t3)] ^ T7[B2(t2)] ^ T8[B3(t1)]; b[1] = T5[B0(t1)] ^ T6[B1(t0)] ^ T7[B2(t3)] ^ T8[B3(t2)]; b[2] = T5[B0(t2)] ^ T6[B1(t1)] ^ T7[B2(t0)] ^ T8[B3(t3)]; b[3] = T5[B0(t3)] ^ T6[B1(t2)] ^ T7[B2(t1)] ^ T8[B3(t0)]; }
        t0 = b[0] ^ ctx.rk[1][0]; t1 = b[1] ^ ctx.rk[1][1]; t2 = b[2] ^ ctx.rk[1][2]; t3 = b[3] ^ ctx.rk[1][3]; b[0] = S5[B0(t0)] | (S5[B1(t3)] << 8) | (S5[B2(t2)] << 16) | (S5[B3(t1)] << 24); b[1] = S5[B0(t1)] | (S5[B1(t0)] << 8) | (S5[B2(t3)] << 16) | (S5[B3(t2)] << 24); b[2] = S5[B0(t2)] | (S5[B1(t1)] << 8) | (S5[B2(t0)] << 16) | (S5[B3(t3)] << 24); b[3] = S5[B0(t3)] | (S5[B1(t2)] << 8) | (S5[B2(t1)] << 16) | (S5[B3(t0)] << 24); b[0] ^= ctx.rk[0][0]; b[1] ^= ctx.rk[0][1]; b[2] ^= ctx.rk[0][2]; b[3] ^= ctx.rk[0][3]; return unpackBytes(b);
    }
    this.Test = function () {
        var key = [0x6B, 0x65, 0x79]
        var data = [0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38]
        var ciph = [0xED, 0xFD, 0x61, 0xCA, 0xBC, 0x18, 0xC4, 0xFE]; var encrypted = this.Encrypt(key, data); var decrypted = this.Decrypt(key, encrypted); isSuccess = true; for (var i = 0; i < data.length; i++) { if (ciph[i] != encrypted[i] || data[i] != decrypted[i]) { isSuccess = false; break; } }
        return isSuccess;
    }
    this.ExpandKey = function (key, bits) { }
    this.Encrypt = function (key, input, mode) {
        if (arguments.length == 2) { }
        var i, aBlock; var bpb = this.BlockSize / 8; var output = new Array(); if (!key || !input) return; if (key.length * 8 != this.KeySize) return; var expandedKey = new keyExpansion(key); for (var b = 0; b < input.length / bpb; b++) {
            var block = input.slice(b * bpb, (b + 1) * bpb); switch (mode) {
                case System.Security.Cryptography.CipherMode.CBC: for (var i = 0; i < bpb; i++) { block[i] ^= this.IV[b * bpb + i]; }
                    break; default: break;
            }
            var dBlock = AESencrypt(block, expandedKey); output = output.concat(dBlock);
        }
        return output;
    }
    this.Decrypt = function (key, input, mode) {
        var bpb = this.BlockSize / 8; var output = new Array(); var aBlock; var block; if (!key || !input) return; if (key.length * 8 != this.KeySize) return; if (!mode) mode = System.Security.Cryptography.CipherMode.ECB; var expandedKey = new prepare_decryption(key); for (var b = 0; b < input.length / bpb; b++) {
            var block = input.slice(b * bpb, (b + 1) * bpb); var dBlock = AESdecrypt(block, expandedKey); if (mode == System.Security.Cryptography.CipherMode.CBC) { for (var i = 0; i < bpb; i++) { dBlock[i] ^= this.IV[b * bpb + i]; } }
            output = output.concat(dBlock);
        }
        return output;
    }
    function CreateCryptor(rgbKey, rgbIV, encrypt) { var key = (rgbKey) ? rgbKey : this.Key; var newKey = new System.Byte(key.length); System.Buffer.BlockCopy(key, 0, newKey, 0, key.length); var iv = (rgbIV) ? rgbIV : this.IV; var newIv = new System.Byte(iv.length); System.Buffer.BlockCopy(iv, 0, newIv, 0, iv.length); var algorithm = new System.Security.Cryptography.RijndaelManaged(); algorithm.Key = newKey; algorithm.IV = newIv; algorithm.Mode = this.Mode; algorithm.Padding = this.Padding; var cryptor = new System.Security.Cryptography.ICryptoTransform(algorithm, encrypt); return cryptor; }
    this.CreateEncryptor = function (rgbKey, rgbIV) { return CreateCryptor.call(this, rgbKey, rgbIV, true); }
    this.CreateDecryptor = function (rgbKey, rgbIV) { return CreateCryptor.call(this, rgbKey, rgbIV, false); }
    this.GenerateIV = function () { this.IV = new Array(16); rng.GetBytes(this.IV); }
    this.GenerateKey = function () { this.Key = new Array(this.KeySize / 8); rng.GetBytes(this.Key); }
    function InitTables() { var ROOT = 0x11B; var s, s2, s3; var i2, i4, i8, i9, ib, id, ie, t; var length = S.length; for (var i = 0; i < length; i++) { s = S[i] & 0xFF; S5[s] = i; s2 = s << 1; if (s2 >= 0x100) s2 ^= ROOT; s3 = s2 ^ s; i2 = i << 1; if (i2 >= 0x100) i2 ^= ROOT; i4 = i2 << 1; if (i4 >= 0x100) i4 ^= ROOT; i8 = i4 << 1; if (i8 >= 0x100) i8 ^= ROOT; i9 = i8 ^ i; ib = i9 ^ i2; id = i9 ^ i4; ie = i8 ^ i4 ^ i2; T1[i] = System.BitConverter.ToInt32([s2, s, s, s3], 0); T2[i] = System.BitConverter.ToInt32([s3, s2, s, s], 0); T3[i] = System.BitConverter.ToInt32([s, s3, s2, s], 0); T4[i] = System.BitConverter.ToInt32([s, s, s3, s2], 0); t = System.BitConverter.ToInt32Be([ib, id, i9, ie], 0); T5[s] = t; U1[i] = t; t = System.BitConverter.ToInt32([ib, ie, i9, id], 0); T6[s] = t; U2[i] = t; t = System.BitConverter.ToInt32([id, ib, ie, i9], 0); T7[s] = t; U3[i] = t; t = System.BitConverter.ToInt32([i9, id, ib, ie], 0); T8[s] = t; U4[i] = t; } }
    this.Initialize = function () { rng = new System.Security.Cryptography.RNGCryptoServiceProvider(); InitTables(); this.GenerateIV(); this.GenerateKey(); }
    this.Initialize.apply(this, arguments);
}
System.Type.RegisterNamespace("System.Security.Cryptography"); System.Security.Cryptography.SHA1 = function () {
    this.Type = "System.Security.Cryptography.SHA1"; this.Name = "SHA1"; this.chrsz = 8; this._buffer = new System.Byte(); this._count = 0; this._expandedBuffer = new Array(); this._stateSHA1 = new Array(); this.ComputeHashAsHex = function (value) { var bytes = this.ComputeHash(value); return System.BitConverter.ToString(bytes, ''); }
    this.ComputeHashAsBase64 = function (value) { var bytes = this.ComputeHash(value); return System.Convert.ToBase64String(bytes, false); }
    this._HashData = function (partIn, ibStart, cbSize) {
        var count = cbSize; var srcOffset = ibStart; var dstOffset = this._count & 0x3f; this._count += count; if ((dstOffset > 0) && ((dstOffset + count) >= 0x40))
        { System.Buffer.BlockCopy(partIn, srcOffset, this._buffer, dstOffset, 0x40 - dstOffset); srcOffset += 0x40 - dstOffset; count -= 0x40 - dstOffset; this.SHATransform(this._expandedBuffer, this._stateSHA1, this._buffer); dstOffset = 0; }
        while (count >= 0x40)
        { System.Buffer.BlockCopy(partIn, srcOffset, this._buffer, 0, 0x40); srcOffset += 0x40; count -= 0x40; this.SHATransform(this._expandedBuffer, this._stateSHA1, this._buffer); }
        if (count > 0)
        { System.Buffer.BlockCopy(partIn, srcOffset, this._buffer, dstOffset, count); }
    }
    this.HashCore = function (rgb, ibStart, cbSize)
    { this._HashData(rgb, ibStart, cbSize); }
    this._EndHash = function () {
        var block = new System.Byte(20); var num = 0x40 - this._count & 0x3f; if (num <= 8) num += 0x40; var partIn = new System.Byte(num); partIn[0] = 0x80; var num2 = this._count * 0x8; var n = num2; for (var i = 1; i <= 8; i++) { partIn[num - i] = n & 0xff; n = n >> 0x08; }
        this._HashData(partIn, 0, partIn.length); this.DWORDToBigEndian(block, this._stateSHA1, 5); this.HashValue = block; return block;
    }
    this.HashFinal = function ()
    { return this._EndHash(); }
    this.SHATransform = function (expandedBuffer, state, block) {
        this.DWORDFromBigEndian(expandedBuffer, 0x10, block); this.SHAExpand(expandedBuffer); var v = new Array(5); var i = 0; for (i = 0; i < 5; i++) v[4 - i] = state[i]; for (i = 0; i < 80; i += 5) {
            for (var j = 0; j < 5; j++)
            { var x0 = _tf(i, v[(j + 3) % 5], v[(j + 2) % 5], v[(j + 1) % 5]); var x1 = as(_rotate(v[(j + 4) % 5], 5), x0); var x2 = expandedBuffer[i + ((j + 0) % 5)]; var x3 = as(x1, x2); var x4 = as(x3, _ac(i)); var x5 = v[(j + 0) % 5]; var x6 = as(x4, x5); v[(j + 0) % 5] = x6; v[(j + 3) % 5] = _rotate(v[(j + 3) % 5], 30); }
        }
        for (i = 0; i < 5; i++) state[i] = as(state[i], v[4 - i]);
    }
    function as(x, y)
    { var lsw = (x & 0xFFFF) + (y & 0xFFFF); var msw = (x >> 16) + (y >> 16) + (lsw >> 16); return (msw << 16) | (lsw & 0xFFFF); }
    function _rotate(num, cnt)
    { return (num << cnt) | (num >>> (32 - cnt)); }
    function _tf(i, b, c, d)
    { return (i < 20) ? (d ^ (b & (c ^ d))) : (i < 40) ? (b ^ c) ^ d : (i < 60) ? (b & c) | (d & (b | c)) : (b ^ c) ^ d; }
    function _ac(i)
    { return (i < 20) ? 0x5A827999 : (i < 40) ? 0x6ED9EBA1 : (i < 60) ? 0x8F1BBCDC : 0xCA62C1D6; }
    this.SHAExpand = function (x) {
        for (var i = 0x10; i < 80; i++)
        { x[i] = _rotate(x[i - 3] ^ x[i - 8] ^ x[i - 14] ^ x[i - 16], 1); }
    }
    this.DWORDFromBigEndian = function (x, digits, block) {
        var index = 0; for (var i = 0; index < digits; i += 4)
        { var n = ((((block[i] << 0x18) | (block[i + 1] << 0x10)) | (block[i + 2] << 8)) | block[i + 3]); x[index] = n; index++; }
    }
    this.DWORDToBigEndian = function (block, x, digits) {
        var index = 0; for (var i = 0; index < digits; i += 4)
        { block[i] = ((x[index] >> 0x18) & 0xff); block[i + 1] = ((x[index] >> 0x10) & 0xff); block[i + 2] = ((x[index] >> 8) & 0xff); block[i + 3] = (x[index] & 0xff); index++; }
    }
    this.Initialize = function ()
    { this.InitializeState(); System.Array.Clear(this._buffer, 0, this._buffer.length); System.Array.Clear(this._expandedBuffer, 0, this._expandedBuffer.length); }
    this.InitializeState = function ()
    { this._count = 0; this._stateSHA1[0] = 0x67452301; this._stateSHA1[1] = 0xefcdab89; this._stateSHA1[2] = 0x98badcfe; this._stateSHA1[3] = 0x10325476; this._stateSHA1[4] = 0xc3d2e1f0; }
    this._initialize = function () {
        var base = new System.Security.Cryptography.HashAlgorithm(); for (var property in base) { if (typeof (this[property]) == "undefined") { this[property] = base[property]; } }
        this.HashSizeValue = 160; this.HashSize = 160; this._stateSHA1 = new System.Byte(5); this._buffer = new System.Byte(0x40); this._expandedBuffer = new System.Byte(80); this.InitializeState();
    }
    this._initialize.apply(this, arguments);
}
System.Security.Cryptography.SHA1CryptoServiceProvider = System.Security.Cryptography.SHA1;