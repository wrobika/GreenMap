var mousePositionControl = new ol.control.MousePosition({
    //coordinateFormat: ol.coordinate.format([x,y],'[{x},{y}]'),
    //projection: 'EPSG:4326',
    // comment the following two lines to have the mouse position
    // be placed within the map.
    //className: 'custom-mouse-position',
    target: document.getElementById('mouse-position'),
    undefinedHTML: '&nbsp;'
});
map.addControl(mousePositionControl);


var drillingLayer;
var hydroizohypseLayer;
for (var layer of layers) {
    if (layer.get('name') === 'drilling')
        drillingLayer = layer;
    if (layer.get('name') === 'hydroizohypse')
        hydroizohypseLayer = layer;
}

var drillingInteraction = new ol.interaction.Select({
    condition: ol.events.condition.click,
    layers: [drillingLayer],
    style: false
});

var hydroizohypseInteraction = new ol.interaction.Select({
    condition: ol.events.condition.pointerMove,
    layers: [hydroizohypseLayer],
    style: function (feature) {
        return new ol.style.Style({
            stroke: new ol.style.Stroke({
                color: feature.get('color'),
                width: 3
            })
        })
    }
});

map.addInteraction(drillingInteraction);
map.addInteraction(hydroizohypseInteraction);

drillingInteraction.on('select', function (e) {
    var selectFeatures = drillingInteraction.getFeatures().getArray();
    if (selectFeatures[0].get('features').length === 1) {
        $.ajax({
            contentType: "application/json; charset=utf-8",
            type: "GET",
            url: "api/Odwiert/" + selectFeatures[0].get('features')[0].getId(),
            success: function (point) { fillInfoModal(point) },
            error: function (jqXHR) { console.log(jqXHR) }
        });
    }
});

hydroizohypseInteraction.on('select', function (e) {
    if (e.selected.length === 1) {
        var coordinateString = document.getElementById('mouse-position').textContent;
        var coordinateArray = coordinateString.split(',');
        console.log(coordinateArray);
        container.innerHTML = e.selected[0].get('color');
        overlay.setPosition(coordinateArray);
    } else {
        overlay.setPosition(undefined);
        return false;
    }
});

function fillInfoModal(point) {
    document.getElementById('info').click();
    document.getElementById('infoName').innerText = point.nazwaObiektu;
    document.getElementById('infoRbdh').innerText = point.nrRbdh;
    document.getElementById('infoStatus').innerText = point.status;
    document.getElementById('infoDistrict').innerText = point.lokalizacja;
    document.getElementById('infoCoord').innerText = point.wspolrzedne;
    document.getElementById('infoDepth').innerText = point.glebokoscZwierciadla;
    document.getElementById('infoFilterClass').innerText = point.klasaFiltracji;
    document.getElementById('infoFilter').innerText = point.filtracja;
    document.getElementById('infoHydro').innerText = point.hydroGleby;
    document.getElementById('infoImpurity').innerText = point.zanieczyszczenieGleby;
    document.getElementById('infoQuality').innerText = point.jakoscWody;
    document.getElementById('infIrrigation').innerText = point.nawodnienie;
    document.getElementById('infoProfile').innerText = point.profil;
}