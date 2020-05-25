function createSearchModel() {
    var searchModel = {};
    searchModel.NazwaObiektu = $('#NazwaObiektu').val();
    searchModel.NrRbdh = $('#NrRbdh').val();
    searchModel.Lokalizacja = $('#Lokalizacja').val();
    searchModel.Status = $('#Status').val();
    searchModel.EurefX1 = $('#EurefX1').val();
    searchModel.EurefX2 = $('#EurefX2').val();
    searchModel.EurefY1 = $('#EurefY1').val();
    searchModel.EurefY2 = $('#EurefY2').val();
    searchModel.GlebokoscZwierciadla1 = $('#GlebokoscZwierciadla1').val();
    searchModel.GlebokoscZwierciadla2 = $('#GlebokoscZwierciadla2').val();
    searchModel.Filtracja1 = $('#Filtracja1').val();
    searchModel.Filtracja2 = $('#Filtracja2').val();
    searchModel.HydroGleby = $('#HydroGleby').val();
    searchModel.ZanieczyszczenieGleby = $('#ZanieczyszczenieGleby').val();
    searchModel.JakoscWody = $('#JakoscWody').val();
    searchModel.Nawodnienie = $('#Nawodnienie').val();
    return JSON.stringify(searchModel);
}

function search() {
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: 'api/Odwiert',
        data: createSearchModel(),
        cache: false,
        success: function (data) { loadFeatures(data) },
        error: function (jqXHR) { console.log(jqXHR) }
    });
    $('#searchModal').modal('hide');
}

function searchRemove() {
    $.ajax({
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: 'api/Odwiert',
        cache: false,
        success: function (data) { loadFeatures(data) },
        error: function (jqXHR) { console.log(jqXHR) }
    });
    $('#searchModal').modal('hide');
}

function loadFeatures(data) {
    map.getLayers().forEach(function (layer) {
        if (layer.get('name') === 'odwierty') {
            var featuresArray = [];
            for (var id of Object.keys(data)) {
                var feature = wktReader.readFeature(data[id]);
                feature.getGeometry().transform('EPSG:2180', 'EPSG:3857');
                feature.set('color', layerProperties['odwierty'].color);
                feature.setId(id);
                featuresArray.push(feature);
            }
            layer.getSource().getSource().clear();
            layer.getSource().getSource().addFeatures(featuresArray);
        }
    });
}

let searchSubmitButton = document.getElementById('searchSubmit');
searchSubmitButton.addEventListener("click", search);
let searchRemoveButton = document.getElementById('searchRemove');
searchRemoveButton.addEventListener("click", searchRemove);
