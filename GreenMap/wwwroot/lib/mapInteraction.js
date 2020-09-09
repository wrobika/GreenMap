var popup = document.getElementById('popup');

var overlay = new ol.Overlay({
    element: popup,
    autoPan: true,
    autoPanAnimation: {
        duration: 250
    }
});
map.addOverlay(overlay);

var mousePositionControl = new ol.control.MousePosition({
    target: document.getElementById('mouse-position'),
    undefinedHTML: '&nbsp;'
});
map.addControl(mousePositionControl);

var drillingLayer;
var hydroizohypseLayer;
var monitoringLayer;
var soilPollutionLayer;
var chemistryLayer;
for (var layer of layers) {
    switch (layer.get('name')) {
        case 'drilling': drillingLayer = layer; break;
        case 'hydroizohypse': hydroizohypseLayer = layer; break;
        case 'monitoring': monitoringLayer = layer; break;
        case 'soilPollution': soilPollutionLayer = layer; break;
        case 'groundwaterChemistry': chemistryLayer = layer; break;
    }
}

var drillingInteraction = getClickInteraction(drillingLayer);
var monitoringInteraction = getClickInteraction(monitoringLayer);
var soilPollutionInteraction = getClickInteraction(soilPollutionLayer);
var chemistryInteraction = getClickInteraction(chemistryLayer);

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

function getClickInteraction(layer) {
    return new ol.interaction.Select({
        condition: ol.events.condition.click,
        layers: [layer],
        style: false
    });
}

hydroizohypseInteraction.on('select', function (event) {
    if (event.selected.length === 1) {
        var coordinateString = document.getElementById('mouse-position').textContent;
        var coordinateArray = coordinateString.split(',');
        popup.innerHTML = event.selected[0].get('depth');
        overlay.setPosition(coordinateArray);
    } else {
        overlay.setPosition(undefined);
        return false;
    }
});

monitoringInteraction.on('select', function (event) {
    var singleFeature = getSingleFeature(event);
    if (singleFeature) {
        const pdfName = singleFeature.get('pdfName');
        window.open("api/Monitoring/" + pdfName);
    }
});

drillingInteraction.on('select', function (event) {
    getInfo(event, "api/Odwiert/", fillDrillingModal);
});

soilPollutionInteraction.on('select', function (event) {
    getInfo(event, "api/ZanieczyszczenieGleb/", fillSoilPollutionModal);
});

chemistryInteraction.on('select', function (event) {
    getInfo(event, "api/SkladChemicznyWodPodziemnych/", fillChemistryModal);
});

function getInfo(event, url, fillModalFunction) {
    var singleFeature = getSingleFeature(event);
    if (singleFeature) {
        $.ajax({
            contentType: "application/json; charset=utf-8",
            type: "GET",
            url: url + singleFeature.getId(),
            success: function (point) { fillModalFunction(point) },
            error: function (jqXHR) { console.log(jqXHR) }
        });
    }
}

function getSingleFeature(event) {
    if (event.selected.length) {
        var selectedFeatures = event.selected[0].get('features')
        if (selectedFeatures.length === 1)
            return selectedFeatures[0];
        else
            return null;
    }
    return null;
}

function fillDrillingModal(point) {
    document.getElementById('infoName').innerText = point.nazwaObiektu;
    document.getElementById('infoRbdh').innerText = point.nrRbdh;
    document.getElementById('infoStatus').innerText = point.status;
    document.getElementById('infoDistrict').innerText = point.lokalizacja;
    document.getElementById('infoCoord').innerText = point.wspolrzedne;
    document.getElementById('infoDepth').innerText = point.glebokoscZwierciadla;
    document.getElementById('infoFilterClass').innerText = point.klasaFiltracji;
    document.getElementById('infoFilter').innerText = point.filtracja;
    document.getElementById('infoPdf').setAttribute('href', '/api/Odwiert/OpenProfilePdf/' + point.nrRbdh);
    document.getElementById('infoDrilling').click();
}

function fillSoilPollutionModal(point) {
    document.getElementById('infoSoilPollutionSymbol').innerText = point.symbol;
    document.getElementById('infoSoilPollutionCoord').innerText = point.x +' '+ point.y +' '+ point.z;
    document.getElementById('infoSoilPollutionDate').innerText = point.dataOprobowania;
    document.getElementById('infoSoilPollutionGroup').innerText = point.grupaGruntow;
    document.getElementById('infoSoilPollutionExist').innerText = point.zanieczyszczenieGleby0025;
    document.getElementById('infoSoilPollutionSubstance').innerText = point.substancjeStwarzajaceRyzyko0025;
    document.getElementById('infoGroundPollution').innerText = point.zanieczyszczenieGruntu025100;
    document.getElementById('infoGroundPollutionSubstance').innerText = point.substancjeStwarzajaceRyzyko025100;
    document.getElementById('infoDeepGroundPollution').innerText = point.zanieczyszczenieGruntu100;
    document.getElementById('infoDeepGroundPollutionSubstance').innerText = point.substancjeStwarzajaceRyzyko100;
    document.getElementById('infoSoilPollution').click();
}

function fillChemistryModal(point) {
    document.getElementById('infoChemistrySymbol').innerText = point.symbolPunktu;
    document.getElementById('infoChemistryCoord').innerText = point.x + ' ' + point.y + ' ' + point.rzednaTerenu;
    document.getElementById('infoChemistryDate').innerText = point.dataBadania;
    document.getElementById('infoChemistryPh').innerText = point.ph;
    document.getElementById('infoChemistryPew').innerText = point.pew;
    document.getElementById('infoChemistrySar').innerText = point.sar;
    document.getElementById('infoChemistryQuality').innerText = point.klasaJakosci;
    document.getElementById('infoChemistryIrrigation').innerText = point.przydatnoscDoNawadniania;
    document.getElementById('infoChemistry').click();
}

map.addInteraction(drillingInteraction);
map.addInteraction(hydroizohypseInteraction);
map.addInteraction(monitoringInteraction);
map.addInteraction(soilPollutionInteraction);
map.addInteraction(chemistryInteraction);