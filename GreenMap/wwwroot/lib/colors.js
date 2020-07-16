function rgba(layerName) {
    var layer = layerProperties[layerName];
    var color = layer.color;
    var alpha = layer.opacity;
    const [r, g, b] = Array.from(ol.color.asArray(color));
    return ol.color.asString([r, g, b, alpha]);
}

function getDepthColor(maxDepth, minDepth, actualDepth) {
    var range = maxDepth - minDepth;
    var value = actualDepth - minDepth;
    var green = (255 * value) / range;
    var blue = 255 - green;
    return "rgb(0," + green + "," + blue + ")";
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
        case "C": return "Black";
        case "D": return "Black";
        default: return "White";
    }
}