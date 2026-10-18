/*
 * Selector de horas para citas.
 * - Horario de atencion: 8:00 a.m. a 5:00 p.m. (cada 30 minutos).
 * - Oculta las horas que el doctor ya tiene ocupadas en la fecha elegida.
 * - Si una cita se cancela (o se rechaza), su hora vuelve a aparecer.
 */
window.DentalCareHorarios = (function () {
    var HORA_INICIO = 8 * 60;   // 08:00
    var HORA_FIN = 17 * 60;     // 17:00 (ultima hora disponible)
    var INTERVALO = 30;         // minutos

    function dos(n) { return ('0' + n).slice(-2); }

    function generarHoras() {
        var horas = [];
        for (var m = HORA_INICIO; m <= HORA_FIN; m += INTERVALO) {
            horas.push(dos(Math.floor(m / 60)) + ':' + dos(m % 60));
        }
        return horas;
    }

    function etiqueta(hhmm) {
        var partes = hhmm.split(':');
        var h = parseInt(partes[0], 10);
        var h12 = (h % 12 === 0) ? 12 : (h % 12);
        return h12 + ':' + partes[1] + ' ' + (h >= 12 ? 'p.m.' : 'a.m.');
    }

    function iniciar(opc) {
        var selHora = document.getElementById(opc.hora);
        var doctor = document.getElementById(opc.doctor);
        var fecha = document.getElementById(opc.fecha);
        var aviso = document.getElementById(opc.aviso);
        if (!selHora || !doctor || !fecha) return null;

        var valorInicial = selHora.getAttribute('data-valor') || '';

        function pintar(ocupadas) {
            var actual = selHora.value || valorInicial;
            valorInicial = '';

            selHora.innerHTML = '<option value="">-- Seleccione hora --</option>';
            var disponibles = 0;
            generarHoras().forEach(function (h) {
                if (ocupadas.indexOf(h) !== -1) return;   // hora ocupada: no se muestra
                var op = document.createElement('option');
                op.value = h;
                op.textContent = etiqueta(h);
                selHora.appendChild(op);
                disponibles++;
            });

            if (actual && selHora.querySelector('option[value="' + actual + '"]')) {
                selHora.value = actual;
            }

            if (!aviso) return;
            if (!doctor.value || !fecha.value) {
                aviso.textContent = 'Horario de atencion: 8:00 a.m. a 5:00 p.m. Seleccione doctor y fecha para ver las horas disponibles.';
            } else if (disponibles === 0) {
                aviso.textContent = 'El doctor no tiene horarios disponibles en esa fecha.';
            } else {
                aviso.textContent = 'Horas disponibles: ' + disponibles + '. Las horas ocupadas no se muestran y vuelven a aparecer si la cita se cancela.';
            }
        }

        function cargar() {
            if (!doctor.value || !fecha.value) { pintar([]); return; }
            var url = opc.url +
                '?idDoctor=' + encodeURIComponent(doctor.value) +
                '&fecha=' + encodeURIComponent(fecha.value);
            if (opc.idCita) url += '&idCita=' + encodeURIComponent(opc.idCita);
            fetch(url, { credentials: 'same-origin' })
                .then(function (r) { return r.json(); })
                .then(pintar)
                .catch(function () { pintar([]); });
        }

        doctor.addEventListener('change', cargar);
        fecha.addEventListener('change', cargar);
        fecha.addEventListener('input', cargar);
        cargar();

        return { recargar: cargar };
    }

    return { iniciar: iniciar };
})();
