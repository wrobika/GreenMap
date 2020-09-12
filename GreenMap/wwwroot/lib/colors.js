function rgba(layerName) {
    var layer = layerProperties[layerName];
    var color = layer.color;
    var alpha = layer.opacity;
    const [r, g, b] = Array.from(ol.color.asArray(color));
    return ol.color.asString([r, g, b, alpha]);
}

function getFeatureColor(featureName, layerName) {
    switch (layerName) {
        case "filter": return getFilteringColor(featureName);
        case "district": return getDistrictColor(featureName);
        default: return "Black";
    }
}

function getFeatureTextColor(featureName, layerName) {
    switch (layerName) {
        case "filter": return getFilteringTextColor(featureName);
        default: return "Black";
    }
}

function getDepthColor(maxDepth, minDepth, actualDepth) {
    var range = maxDepth - minDepth;
    var value = actualDepth - minDepth;
    var blue = 255 - (255 * value) / range;
    return "rgb(0,0," + blue + ")";
}

function getFilteringColor(filteringClass) {
    switch (filteringClass) {
        case "A": return "DarkBlue";
        case "B": return "Blue";
        case "C": return "Yellow";
        case "D": return "Orange";
        case "E": return "Red";
        default: return "Black";
    }
}

function getFilteringTextColor(filteringClass) {
    switch (filteringClass) {
        case "C":
        case "D":
        case "E": return "Black";
        default: return "White";
    }
}

function getDistrictColor(name) {
    var color;
    var alpha = '0.5';
    switch (name) {
        case "Dzielnica XII Bieżanów-Prokocim": color = "Tomato"; break;
        case "Dzielnica XIII Podgórze": color = "Gold"; break;
        case "Dzielnica XIV Czyżyny": color = "Magenta"; break;
        case "Dzielnica XVI Bieńczyce": color = "Yellow"; break;
        case "Dzielnica XVIII Nowa Huta": color = "DarkOrange"; break;
        default: color = "Gold"; break;
    }
    const [r, g, b] = Array.from(ol.color.asArray(color));
    return ol.color.asString([r, g, b, alpha]);
}