function rgba(layerName) {
    var layer = layerProperties[layerName];
    var color = layer.color;
    var alpha = layer.opacity;
    const [r, g, b] = Array.from(ol.color.asArray(color));
    return ol.color.asString([r, g, b, alpha]);
}

proj4.defs('EPSG:2180', '+proj=tmerc + lat_0=0 + lon_0=19 + k=0.9993 + x_0=500000 + y_0=-5300000 + ellps=GRS80 + towgs84=0, 0, 0, 0, 0, 0, 0 + units=m + no_defs');
proj4.defs('EPSG:2178', '+proj=tmerc +lat_0=0 +lon_0=21 +k=0.999923 +x_0=7500000 +y_0=0 +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs ');
ol.proj.proj4.register(proj4);
var wktReader = new ol.format.WKT();

function getLayerSource(layerName) {
    var objects = layerObjects[layerName];
    var featuresArray = [];
    if (Array.isArray(objects)) {
        for (var wkt of objects) {
            var feature = wktReader.readFeature(wkt);
            feature.getGeometry().transform('EPSG:2180', 'EPSG:3857');
            feature.set('color', layerProperties[layerName].color);
            featuresArray.push(feature);
        }
    }
    else {
        if (layerName === 'filter') {
            for (var wkt of Object.keys(objects)) {
                var feature = wktReader.readFeature(wkt);
                feature.getGeometry().transform('EPSG:2180', 'EPSG:3857');
                feature.set('color', objects[wkt]);
                featuresArray.push(feature);
            }
        }
        if (layerName === 'hydroizohypse') {
            for (var wkt of Object.keys(objects)) {
                var feature = wktReader.readFeature(wkt);
                feature.getGeometry().transform('EPSG:2178', 'EPSG:3857');
                var depthColor = objects[wkt];
                feature.set('color', depthColor);
                featuresArray.push(feature);
            }
        }
        if (layerName === 'drilling') {
            for (var id of Object.keys(objects)) {
                var feature = wktReader.readFeature(objects[id]);
                feature.getGeometry().transform('EPSG:2180', 'EPSG:3857');
                feature.set('color', layerProperties[layerName].color);
                feature.setId(id);
                featuresArray.push(feature);
            }
        }
    }
    return new ol.source.Vector({
        features: featuresArray
    });
}

function getLayer(layerName) {
    if (layerProperties[layerName].cluster === true) {
        return getClusterLayer(layerName);
    }
    return new ol.layer.Vector({
        name: layerName,
        visible: layerProperties[layerName].visible,
        source: getLayerSource(layerName),
        style: function (feature) {
            return getStyle(feature, layerName);
        }
    });
}

function getStyle(feature, layerName) {
    var layerStroke = null;
    if (layerName === 'city') {
        layerStroke = new ol.style.Stroke({
            color: layerProperties[layerName].stroke,
            width: 2
        });
    }

    var layerFill = new ol.style.Fill({
        color: rgba(layerName)
    });
    if (layerName === 'hydroizohypse') {
        layerStroke = new ol.style.Stroke({
            color: feature.get('color'),
            width: 1
        });
    }
    if (layerName === 'filter') {
        layerFill = new ol.style.Fill({
            color: feature.get('color'),
        });
    }

    return new ol.style.Style({
        stroke: layerStroke,
        fill: layerFill
    })
}

function getClusterLayer(layerName) {
    var objectCluster = new ol.source.Cluster({
        distance: 50,
        source: getLayerSource(layerName)
    });
    return new ol.layer.Vector({
        name: layerName,
        visible: layerProperties[layerName].visible,
        source: objectCluster,
        style: function (feature) {
            var size = feature.get('features').length;
            if (size === 1) {
                style = new ol.style.Style({
                    image: new ol.style.Circle({
                        radius: layerProperties[layerName].radius,
                        fill: new ol.style.Fill({
                            color: feature.get('features')[0].get('color')
                        })
                    })
                })
            }
            else {
                style = new ol.style.Style({
                    image: new ol.style.Circle({
                        radius: layerProperties[layerName].radius,
                        fill: new ol.style.Fill({
                            color: rgba(layerName)
                        })
                    }),
                    text: new ol.style.Text({
                        text: size.toString(),
                        fill: new ol.style.Fill({
                            color: layerProperties[layerName].text
                        })
                    })
                });
            }
            return style;
        }
    });
}