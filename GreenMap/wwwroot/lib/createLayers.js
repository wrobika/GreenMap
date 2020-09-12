proj4.defs('EPSG:2180', '+proj=tmerc + lat_0=0 + lon_0=19 + k=0.9993 + x_0=500000 + y_0=-5300000 + ellps=GRS80 + towgs84=0, 0, 0, 0, 0, 0, 0 + units=m + no_defs');
proj4.defs('EPSG:2178', '+proj=tmerc +lat_0=0 +lon_0=21 +k=0.999923 +x_0=7500000 +y_0=0 +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs ');
ol.proj.proj4.register(proj4);
var wktReader = new ol.format.WKT();

function getLayer(layerName) {
    if (layerObjects[layerName] === WMS) {
        return getWMSLayer(layerName);
    }
    if (layerProperties[layerName].cluster) {
        return getClusterLayer(layerName);
    }
    return getVectorLayer(layerName);
}

function getWMSLayer(layerName) {
    var properties = layerProperties[layerName]
    return new ol.layer.Image({
        name: layerName,
        visible: properties.visible,
        source: new ol.source.ImageWMS({
            url: properties.urlWMS,
            params: { 'LAYERS': properties.layersWMS },
        })
    });
}

function getVectorLayer(layerName) {
    return new ol.layer.Vector({
        name: layerName,
        visible: layerProperties[layerName].visible,
        source: getLayerSource(layerName),
        style: function (feature) {
            return getStyle(feature, layerName);
        }
    });
}

function getClusterLayer(layerName) {
    return new ol.layer.Vector({
        name: layerName,
        visible: layerProperties[layerName].visible,
        source: getClusterSource(layerName),
        style: function (feature) {
            return getClusterStyle(feature, layerName);
        }
    });
}

function getClusterSource(layerName) {
    return new ol.source.Cluster({
        distance: 50,
        source: getLayerSource(layerName)
    });
}

function getLayerSource(layerName) {
    return new ol.source.Vector({
        features: getFeatures(layerName)
    });
}

function getFeatures(layerName) {
    var objects = layerObjects[layerName];
    switch (layerName) {
        case 'district':
        case 'filter': return getNamedFeatures(objects, layerName);
        case 'hydroizohypse': return getHydroizohypseFeatures(objects);
        case 'monitoring': return getMonitoringFeatures(objects, layerName);
        case 'soilPollution':
        case 'groundwaterChemistry':
        case 'drilling': return getFeaturesWithId(objects, layerName);
        default: return getSimpleFeatures(objects, layerName);
    }
}

function getSimpleFeatures(objects, layerName) {
    var featuresArray = [];
    for (var wkt of objects) {
        var feature = wktReader.readFeature(wkt);
        feature.getGeometry().transform('EPSG:2180', 'EPSG:3857');
        feature.set('color', layerProperties[layerName].color);
        featuresArray.push(feature);
    }
    return featuresArray;
}

function getNamedFeatures(objects, layerName) {
    var featuresArray = [];
    for (var wkt of Object.keys(objects)) {
        var feature = wktReader.readFeature(wkt);
        feature.getGeometry().transform('EPSG:2180', 'EPSG:3857');
        var featureName = objects[wkt];
        var featureColor = getFeatureColor(featureName, layerName);
        var textColor = getFeatureTextColor(featureName, layerName);
        feature.set('color', featureColor);
        feature.set('textColor', textColor); 
        feature.set('name', featureName);
        featuresArray.push(feature);
    }
    return featuresArray;
}

function getHydroizohypseFeatures(objects) {
    var featuresArray = [];
    var depthValues = Object.values(objects);
    var maxDepth = Math.max.apply(Math, depthValues);
    var minDepth = Math.min.apply(Math, depthValues);
    for (var wkt of Object.keys(objects)) {
        var feature = wktReader.readFeature(wkt);
        feature.getGeometry().transform('EPSG:2178', 'EPSG:3857');
        var depth = objects[wkt];
        var depthColor = getDepthColor(maxDepth, minDepth, depth);
        feature.set('color', depthColor);
        feature.set('depth', depth);
        featuresArray.push(feature);
    }
    return featuresArray;
}

function getFeaturesWithId(objects, layerName) {
    var featuresArray = [];
    for (var id of Object.keys(objects)) {
        var feature = wktReader.readFeature(objects[id]);
        feature.getGeometry().transform('EPSG:2180', 'EPSG:3857');
        feature.set('color', layerProperties[layerName].color);
        feature.setId(id);
        featuresArray.push(feature);
    }
    return featuresArray;
}

function getMonitoringFeatures(objects, layerName) {
    var featuresArray = [];
    for (var wkt of Object.keys(objects)) {
        var feature = wktReader.readFeature(wkt);
        feature.getGeometry().transform('EPSG:2178', 'EPSG:3857');
        feature.set('color', layerProperties[layerName].color);
        feature.set('pdfName', objects[wkt]);
        featuresArray.push(feature);
    }
    return featuresArray;
}

function getStyle(feature, layerName) {
    return new ol.style.Style({
        stroke: getStroke(feature, layerName),
        fill: getFill(false, feature, layerName),
        text: getText(feature, layerName)
    })
}

function getClusterStyle(feature, layerName) {
    var size = feature.get('features').length;
    if (size === 1)
        return getSinglePointStyle(feature, layerName);
    else
        return getMultiPointStyle(feature, layerName);
}

function getSinglePointStyle(feature, layerName) {
    var singleFeature = feature.get('features')[0];
    return new ol.style.Style({
        image: getImage(feature, layerName),
        text: getText(singleFeature, layerName)
    })
}

function getMultiPointStyle(feature, layerName) {
    var size = feature.get('features').length;
    return new ol.style.Style({
        image: getImage(feature, layerName),
        text: new ol.style.Text({
            text: size.toString(),
            fill: new ol.style.Fill({
                color: layerProperties[layerName].text
            })
        })
    });
}

function getImage(feature, layerName) {
    var properties = layerProperties[layerName];
    var radius = properties.radius;
    var fill = getFill(true, feature, layerName);
    var stroke = getStroke(feature, layerName)

    if (properties.iconShapePoints !== 0) {
        return new ol.style.RegularShape({
            points: properties.iconShapePoints,
            radius: radius,
            fill: fill,
            stroke: stroke
        });
    }
    return new ol.style.Circle({
        radius: radius,
        fill: fill,
        stroke: stroke
    });
}

function getStroke(feature, layerName) {
    var color;
    var width = 1;
    switch (layerName) {
        case 'city': color = layerProperties[layerName].stroke; width = 2; break;
        case 'hydroizohypse': color = feature.get('color'); break;
        case 'drilling':
        case 'filter':
        case 'soilPollution':
        case 'groundwaterChemistry': color = layerProperties[layerName].text; break;
        case 'monitoring': color = layerProperties[layerName].text; width = 2; break;
        default: return null;
    }
    return new ol.style.Stroke({
        color: color,
        width: width
    });
}

function getFill(cluster, feature, layerName) {
    if (cluster && feature.get('features').length === 1) {
        var singleFeature = feature.get('features')[0];
        return new ol.style.Fill({
            color: singleFeature.get('color')
        })
    }
    if (layerName === 'district') {
        return new ol.style.Fill({
            color: feature.get('color')
        })
    }
    return new ol.style.Fill({
        color: rgba(layerName)
    });
}

function getText(feature, layerName) {
    if (layerName === 'filter' || layerName === 'district') {
        return new ol.style.Text({
            text: feature.get('name'),
            fill: new ol.style.Fill({
                color: feature.get('textColor')
            })
        });
    }
    return null;
}