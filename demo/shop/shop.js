'use strict';

/** More at https://github.com/emoreno911/UI-to-Code **/
var $wrap = document.querySelector('.wrap'),
    $opts = document.querySelectorAll('.bar a');

Array.prototype.forEach.call($opts, function (el, i) {
    el.addEventListener('click', function (event) {
        $wrap.setAttribute('data-pos', i);
    });
});

var count = 0,
    $btns = document.querySelectorAll('.btn'),
    $cart = document.querySelector('.cart');

Array.prototype.forEach.call($btns, function (el, i) {
    el.addEventListener('click', function (event) {
        $cart.classList.add('add');
        $cart.querySelector('span').innerText = ++count;
        setTimeout(function () {
            $cart.classList.remove('add');
        }, 1500);
    });
});

//document.querySelector('.wrap-clip').style.borderTopWidth = window.innerHeight * .8 + 'px';
//# sourceURL=pen.js