window.onload = function () {

    var img = document.getElementById('img');

    var container = document.getElementById('container');

    var showImage = function showImage() {
        img.style.display = "inline";
        container.style.backgroundImage = "";
    };

    img.addEventListener('load', showImage);
    img.addEventListener('error', showImage);


    var gifs = document.getElementsByClassName('gif');

    for (var i = 0, z = gifs.length; i < z; i++) {

        var gif = gifs[i];

        var handler = (function (t) {

            return function clickGif() {

                container.style.backgroundImage = "url('https://dummyimage.com/500x500/000/fff.gif&text=loading')";
                img.style.display = "none";
                img.src = t.dataset['image'];
            };
        })(gif);
        gif.addEventListener('click', handler);
    }
};