function hideContent(type) {
    if (type == 'yes') {

        document.getElementById('noedt').style.display = 'none';
        document.getElementById('edt').style.display = 'block';
    }
    else {
        document.getElementById('edt').style.display = 'none';
        document.getElementById('noedt').style.display = 'block';
    }
}


