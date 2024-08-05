

// Metodo para visualizar una animacion cuando se esta cargando algun contenido en la pagína
// Tambien deshabilita los controles que sean pasado en controlesParaDeshabilitar
function MostrarAnimacionCargando(controlAnimacion, controlesParaDeshabilitar) {
    var ctrParaDeshabilitar = null;
    var controles = controlesParaDeshabilitar.split(',');

    for (var i = 0; i < controles.length; i++) {
        try {
            ctrParaDeshabilitar = document.getElementById(controles[i]);
            if (ctrParaDeshabilitar) ctrParaDeshabilitar.style.display = 'none';
        } catch (e) { }
    }
    if (controlAnimacion != '{0}'){
    var divLoading = document.getElementById(controlAnimacion);
    divLoading.style.display = 'block';
    }
    return true;
}


// Metodo para el proceso de expandir o contraer un sector HTML enmarcado en un DIV
function Expandir_Contraer(controlExpand, controlDiv) {
    var unDiv = document.getElementById(controlDiv);
    if (unDiv.style.display == 'block') {
        //controlExpand.src = iconExpandMax;
        unDiv.style.display = 'none';
      
    } else {
        //controlExpand.src = iconExpandMin;
        unDiv.style.display = 'block'
    }

    return true;
}

function Contraer_Expandir(controlExpand, controlDiv, iconExpandMax, iconExpandMin) {
    var unDiv = document.getElementById(controlDiv);
    if (unDiv.style.display == 'none') {
        controlExpand.src = iconExpandMin;
        unDiv.style.display = 'block'

    } else {

        controlExpand.src = iconExpandMax;
        unDiv.style.display = 'none'
    }

    return true;
}


function Expandir_ContraerTd(controlExpand, controltd) {
    var unDiv = document.getElementById(controltd);
    
    if (unDiv.style.display == 'block') {
        unDiv.style.display = 'none'
        
    } else {
        unDiv.style.display = 'block'
    }

    setTimeout

    return true;

    }

   


function Contraer_ExpandirTd(controlExpand, controltd, iconExpandMax, iconExpandMin) {
    var unDiv = document.getElementById(controltd);
    if (unDiv.style.display == 'none') {
        controlExpand.src = iconExpandMin;
        unDiv.style.display = 'block'

    } else {

        controlExpand.src = iconExpandMax;
        unDiv.style.display = 'none'
    }

    return true;
}

