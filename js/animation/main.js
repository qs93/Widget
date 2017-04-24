'use strict';

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var textValue = '121';
var textSize_ = 500;
var sampleFactor = 0.25;

var canvas = undefined,
    ctx = undefined;
var font = undefined;
var particles = [];
var mouseIn = false;
var mouseEverIn = false;

var tut = true;
var zeroed = undefined;

function preload() {
    font = loadFont('https://alca.tv/static/codepen/pens/NjPyvP/HelveticaNeueLTStd-Bd.otf');
}

function setup() {
    canvas = createCanvas(windowWidth, windowHeight);
    ctx = canvas.drawingContext;

    colorMode(HSB);

    canvas.canvas.onmouseenter = function () {
        return mouseIn = mouseEverIn = true;
    };
    canvas.canvas.onmouseleave = function () {
        return mouseIn = false;
    };

    zeroed = createVector(0, 0);

    textSize(textSize_);
    textFont(font);
    var w = textWidth(textValue);
    var points = font.textToPoints(textValue, -w / 2, textSize_ / 3, textSize_, { sampleFactor: sampleFactor });

    points.forEach(function (n) {
        var particle = addParticle();
        particle.setPos(n.x, n.y);
        particle.randomize();
    });
}

function addParticle() {
    var particle = new Particle();
    particle.randomize();
    particles.push(particle);
    return particle;
}

function draw() {
    // background(0);
    blendMode(BLEND);
    fill(0, 0.15);
    noStroke();
    rect(0, 0, width, height);

    blendMode(ADD);

    fill(255, 0.02);
    noStroke();

    var center = createVector(width / 2, height / 2);
    var mouse_ = createVector(mouseX, mouseY);
    var mouse = p5.Vector.sub(center, mouse_).rotate(PI);

    translate(center.x, center.y);

    var z = frameCount / 100;
    tut = !mouseEverIn;
    var tutMouse = tut && frameCount < 40;

    particles.forEach(function (n, i) {
        var startPosOffset = p5.Vector.sub(n.startPos, n.pos);
        var mouseDiff = zeroed.copy();
        var noiseInfluence = noise(n.pos.x / 100, n.pos.y / 100, z);
        var noiseForceAngle = map(noiseInfluence, 0.3, 0.7, 0, TAU);
        var noiseForce = p5.Vector.fromAngle(noiseForceAngle);

        if (mouseIn || tutMouse) {
            mouseDiff = p5.Vector.sub(tutMouse ? zeroed.copy() : mouse, n.pos);
            var d = mouseDiff.mag();
            var mouseMaxD = textSize_ / 1.5;
            var inverseDist = mouseMaxD - d;
            if (d >= mouseMaxD) {
                mouseDiff = zeroed.copy();
                fill(255, 0.02);
            } else {
                mouseDiff.mult(inverseDist / mouseMaxD).rotate(PI / (cos(frameCount / 30) * 1.5));
                fill(frameCount * 2 % 360, 255, 255, inverseDist / mouseMaxD);
            }
        }

        n.applyForce(mouseDiff.limit(tutMouse ? 0.6 : 0.8));
        n.applyForce(startPosOffset.limit(0.7));
        n.applyForce(noiseForce.setMag(0.4));

        n.show();
        n.update();
        // n.edges();
    });

    if (tut) {
        stroke(255);
        noFill();
        var a = frameCount * HALF_PI % 100;
        ellipse(0.5, 0.5, 100 - a);
    }
}

function windowResized() {
    resizeCanvas(windowWidth, windowHeight);
}

var Particle = function () {
    function Particle() {
        _classCallCheck(this, Particle);

        this.reset();
    }

    Particle.prototype.applyForce = function applyForce() {
        var _acc;

        (_acc = this.acc).add.apply(_acc, arguments);
    };

    Particle.prototype.reset = function reset() {
        this.pos = zeroed.copy();
        this.acc = zeroed.copy();
        this.vel = zeroed.copy();
        this.setPos(0, 0);
    };

    Particle.prototype.setPos = function setPos(x, y) {
        this.pos.set(x, y);
        this.startPos = this.pos.copy();
    };

    Particle.prototype.randomize = function randomize() {
        this.pos.add(p5.Vector.random2D().mult(10));
        // this.pos.set(random(width), random(height));
    };

    Particle.prototype.update = function update() {
        this.vel.add(this.acc);
        this.acc.mult(0);
        this.pos.add(this.vel);
        this.vel.mult(0.92);
    };

    Particle.prototype.edges = function edges() {
        if (this.pos.x < 0) this.pos.x += width; else if (this.pos.x > width) this.pos.x -= width;
        if (this.pos.y < 0) this.pos.y += height; else if (this.pos.y > height) this.pos.y -= height;
    };

    Particle.prototype.show = function show() {
        var i = arguments.length <= 0 || arguments[0] === undefined ? 0 : arguments[0];

        ellipse(this.pos.x, this.pos.y, 6);
    };

    Particle.prototype.showVel = function showVel() {
        var pVel = this.pos.copy().add(this.vel);
        line(this.pos.x, this.pos.y, pVel.x, pVel.y);
    };

    return Particle;
}();
//# sourceURL=pen.js