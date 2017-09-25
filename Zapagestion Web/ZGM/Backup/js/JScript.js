function Convertir(laCaja) {
    var Datos = laCaja.value.toLowerCase();
    Datos=Datos.replace('w','1');
    Datos=Datos.replace('e','2');
    Datos=Datos.replace('r','3');
    Datos=Datos.replace('s','4');
    Datos=Datos.replace('d','5');
    Datos=Datos.replace('f','6');
    Datos=Datos.replace('z','7');
    Datos=Datos.replace('x','8');
    Datos=Datos.replace('c','9');

    Datos = Datos.replace('a', '*');        //Para la búsqueda

    laCaja.value = Datos;

}