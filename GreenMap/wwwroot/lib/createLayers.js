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
    return new ol.layer.Image({
        name: layerName,
        visible: layerProperties[layerName].visible,
        source: new ol.source.ImageWMS({
            url: layerProperties[layerName].urlWMS,
            params: { 'LAYERS': layerProperties[layerName].layersWMS },
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
        case 'filter': return getFilterFeatures(objects);
        case 'hydroizohypse': return getHydroizohypseFeatures(objects);
        case 'drilling': return getDrillingFeatures(objects, layerName);
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

function getFilterFeatures(objects) {
    var featuresArray = [];
    for (var wkt of Object.keys(objects)) {
        var feature = wktReader.readFeature(wkt);
        feature.getGeometry().transform('EPSG:2180', 'EPSG:3857');
        var filteringAlias = objects[wkt];
        var pointColor = getFilteringColor(filteringAlias);
        var textColor = getFilteringTextColor(filteringAlias);
        feature.set('color', pointColor);
        feature.set('textColor', textColor);
        feature.set('filtering', filteringAlias);
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

function getDrillingFeatures(objects, layerName) {
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

function getStyle(feature, layerName) {
    return new ol.style.Style({
        stroke: getStroke(feature, layerName),
        fill: getFill(feature, layerName),
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
        image: new ol.style.Circle({
            radius: layerProperties[layerName].radius,
            fill: new ol.style.Fill({
                color: singleFeature.get('color')
            })
        }),
        text: getText(singleFeature, layerName)
    })
}

function getMultiPointStyle(feature, layerName) {
    var size = feature.get('features').length;
    return new ol.style.Style({
        image: new ol.style.Circle({
            radius: layerProperties[layerName].radius,
            fill: getFill(feature, layerName)
        }),
        text: new ol.style.Text({
            text: size.toString(),
            fill: new ol.style.Fill({
                color: layerProperties[layerName].text
            })
        })
    });
}

function getStroke(feature, layerName) {
    if (layerName === 'city') {
        return new ol.style.Stroke({
            color: layerProperties[layerName].stroke,
            width: 2
        });
    }
    if (layerName === 'hydroizohypse') {
        return new ol.style.Stroke({
            color: feature.get('color'),
            width: 1
        });
    }
    return null;
}

function getFill(feature, layerName) {
    if (layerName === 'filter') {
        return new ol.style.Fill({
            color: feature.get('color'),
        });
    }
    return new ol.style.Fill({
        color: rgba(layerName)
    });
}

function getText(feature, layerName) {
    if (layerName === 'filter') {
        return new ol.style.Text({
            text: feature.get('filtering'),
            fill: new ol.style.Fill({
                color: feature.get('textColor')
            })
        });
    }
    return null;
}