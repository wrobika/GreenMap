﻿var drillingLayer;
var objectLayer;
for (var layer of layers) {
    if (layer.get('name') === 'otwory hydrogeologiczne')
        drillingLayer = layer;
    if (layer.get('name') === 'obiekty RBDH')
        objectLayer = layer;
}

var drillingInteraction = new ol.interaction.Select({
    condition: ol.events.condition.click,
    layers: [drillingLayer],
    style: false
});

var objectInteraction = new ol.interaction.Select({
    condition: ol.events.condition.pointerMove,
    layers: [objectLayer],
    style: false
});

map.addInteraction(drillingInteraction);
//map.addInteraction(objectInteraction);

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

objectInteraction.on('select', function (e) {
    var selectFeatures = objectInteraction.getFeatures().getArray();
    //document.getElementById('status').innerHTML = '&nbsp;' +
    //    e.target.getFeatures().getLength() +
    //    ' selected features (last operation selected ' + e.selected.length +
    //    ' and deselected ' + e.deselected.length + ' features)';
    if (selectFeatures[0].get('features').length === 1) {
        var coordinate = selectFeatures[0].getGeometry().getCoordinates();
        var hdms = ol.coordinate.toStringHDMS(ol.proj.toLonLat(coordinate));
        content.innerHTML = '<p>You clicked here:</p><code>' + hdms +'</code>';
        overlay.setPosition(coordinate);
    } else {
        overlay.setPosition(undefined);
        closer.blur();
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
    document.getElementById('infoFilter').innerText = point.filtracja;
    document.getElementById('infoHydro').innerText = point.hydroGleby;
    document.getElementById('infoImpurity').innerText = point.zanieczyszczenieGleby;
    document.getElementById('infoQuality').innerText = point.jakoscWody;
    document.getElementById('infIrrigation').innerText = point.nawodnienie;
    document.getElementById('infoProfile').innerText = point.profil;
}