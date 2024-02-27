/*

// get the container's position and dimensions
const container = document.querySelector("#container");
let containerRect = container.getBoundingClientRect();

window.mouse = {
    objRef: null,
    init: init,
    // gets the pointers px coords relative to the viewport
    getAbsMousePos: () => window.absMousePos,
    // gets the pointers coords relative to the svg viewBox and scale
    getSvgMousePos: () =>
        convertToSvgCoords(window.absMousePos.x, window.absMousePos.y),
};

window.mouse.absMousePos = { x: 0, y: 0 };
window.mouse.svgMousePos = { x: 0, y: 0 };

window.mouse.updateDelay = 100;

let mouseState = [false, false, false];

let rafId = null;

function init(objRef) {
    window.mouse.objRef = objRef;
    window.addEventListener("mousedown", mousedown);
    window.addEventListener("mouseup", mouseupevent);

    window.addEventListener("mousemove", (event) => {
        window.absMousePos = { x: event.clientX, y: event.clientY };
        if (!rafId) {
            rafId = window.requestAnimationFrame(() => {
                window.svgMousePos = window.mouse.getSvgMousePos();
                window.mouse.objRef.invokeMethodAsync(
                    "MouseMove",
                    window.svgMousePos.x,
                    window.svgMousePos.y
                );
                rafId = null;
            });
        }
    });
    // window.addEventListener("wheel", wheel);
}

let timeout = null;

function trackMouse(event) {
    if (!timeout) {
        // do calculations here
        setTimeout(() => {
            timeout = null;
        }, window.mouse.updateDelay);
    }
}

function mousedown(event) {
    mouseState[event.button] = true;
    window.mouse.objRef.invokeMethodAsync("MouseDown", event.button);
}

// for some reason, my intellisense complains if I call it mouseup
function mouseupevent(event) {
    mouseState[event.button] = false;
    window.mouse.objRef.invokeMethodAsync("MouseUp", event.button);
}

// hope that people don't resize the window too often
window.addEventListener("resize", () => {
    containerRect = container.getBoundingClientRect();
});

// convert mouse px viewport coords to svg coords
function convertToSvgCoords(x, y) {
    screenWidth = window.innerWidth;
    screenHeight = window.innerHeight;

    // first, convert to px coords relative to container
    // then, scale that in the same way the svg is scaled
    return {
        x: (x - containerRect.x) * (1920 / containerRect.width),
        y: (y - containerRect.y) * (1080 / containerRect.height),
    };
}

*/
