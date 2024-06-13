// define variables for all the pos-presets
// most likely to be null at initialization, but who knows
let leftElement = document.getElementById("pos-preset-left");
let rightElement = document.getElementById("pos-preset-right");
let topElement = document.getElementById("pos-preset-top");
let bottomElement = document.getElementById("pos-preset-bottom");

// define consts for images
// can't be null as they're in index.html
const leftImg = document.getElementById("left-arrow-img");
const rightImg = document.getElementById("right-arrow-img");
const topImg = document.getElementById("up-arrow-img");
const bottomImg = document.getElementById("down-arrow-img");

// this is to check if the preset was shown before the DOM update.
// If it was, you don't have to add new event listeners
let leftWasShown = false;
let rightWasShown = false;
let topWasShown = false;
let bottomWasShown = false;

// app div containing the entire generated blazor stuff
// listen for mutations in that div
const appContainer = document.getElementById("app");

// if mutation occurs, call reassign
let observer = new MutationObserver(function (mutations) {
    reassign();
});

function reassign() {
    // if this is still false at the end, the image and all tracking will be removed
    let isContained = false;

    // get the pos presets again, if they're not null, attach mouseenter event listeners
    // if the preset was shown before, don't add new event listeners
    // if the preset was not shown before, but the cursor is inside the element,
    // show the image and start tracking and add event listeners
    // if it is null, but was shown before, remove the image and tracking

    /*
    <"optimized" but maybe not fully safe version>

    get element

    if visible:
        if not wasShown:
            add event listeners
            if mouse inside:
                show, track
                isContained = true
        wasShown = true

    if not visible:
        if wasShown:
            unshow, untrack
        wasShown = false

    <at end of function>
    <safety measure>
    <should be unnecessary, but just in case>
    if not isContained:
        for all presets: unshow, untrack

    
    <unoptimized but quite safe version>


    for all presets: untrack, unshow

    if visible:
        if not wasShown:
            add event listeners

            if mouse contained:
                isContained = true
                track, show

        wasShown = true
    
    else:
        wasShown = false


    */

    // unshow and untrack all images
    show("left", false);
    track("left", false);

    show("right", false);
    track("right", false);

    show("top", false);
    track("top", false);

    show("bottom", false);
    track("bottom", false);

    leftElement = document.getElementById("pos-preset-left");
    // if visible
    if (leftElement) {
        // if was not shown before
        if (!leftWasShown) {
            leftElement.addEventListener("mouseenter", () => {
                show("left");
                track("left");
                isContained = true;
            });
            leftElement.addEventListener("mouseleave", () => {
                show("left", false);
                track("left", false);
            });
        }
        if (
            !isContained &&
            isPointInsideElement(
                mouse.absMousePos.x,
                mouse.absMousePos.y,
                leftElement
            )
        ) {
            show("left");
            track("left");
            isContained = true;
        }
        leftWasShown = true;
    } else {
        leftWasShown = false;
    }

    rightElement = document.getElementById("pos-preset-right");

    if (rightElement) {
        if (!rightWasShown) {
            rightElement.addEventListener("mouseenter", () => {
                show("right");
                track("right");
                isContained = true;
            });
            rightElement.addEventListener("mouseleave", () => {
                show("right", false);
                track("right", false);
            });
        }
        if (
            !isContained &&
            isPointInsideElement(
                mouse.absMousePos.x,
                mouse.absMousePos.y,
                rightElement
            )
        ) {
            show("right");
            track("right");
            isContained = true;
        }
        rightWasShown = true;
    } else {
        rightWasShown = false;
    }

    topElement = document.getElementById("pos-preset-top");

    if (topElement) {
        if (!topWasShown) {
            topElement.addEventListener("mouseenter", () => {
                show("top");
                track("top");
                isContained = true;
            });
            topElement.addEventListener("mouseleave", () => {
                show("top", false);
                track("top", false);
            });
        }
        if (
            !isContained &&
            isPointInsideElement(
                mouse.absMousePos.x,
                mouse.absMousePos.y,
                topElement
            )
        ) {
            show("top");
            track("top");
            isContained = true;
        }
        topWasShown = true;
    } else {
        topWasShown = false;
    }

    bottomElement = document.getElementById("pos-preset-bottom");

    if (bottomElement) {
        if (!bottomWasShown) {
            bottomElement.addEventListener("mouseenter", () => {
                show("bottom");
                track("bottom");
                isContained = true;
            });
            bottomElement.addEventListener("mouseleave", () => {
                show("bottom", false);
                track("bottom", false);
            });
        }
        if (
            !isContained &&
            isPointInsideElement(
                mouse.absMousePos.x,
                mouse.absMousePos.y,
                bottomElement
            )
        ) {
            show("bottom");
            track("bottom");
            isContained = true;
        }
        bottomWasShown = true;
    } else {
        bottomWasShown = false;
    }

    // safety measure
    if (!isContained) {
        show("left", false);
        track("left", false);

        show("right", false);
        track("right", false);

        show("top", false);
        track("top", false);

        show("bottom", false);
        track("bottom", false);
    }
}

// config for mutation observer
// observes attribute changes, node deletions/insertions in the entire subtree of the app div
let config = { attributes: true, childList: true, subtree: true };

// if start=true, show, if false, stop showing
function show(dir, start = true) {
    switch (dir) {
        case "left":
            start
                ? (leftImg.style.display = "block")
                : (leftImg.style.display = "none");
            break;
        case "right":
            start
                ? (rightImg.style.display = "block")
                : (rightImg.style.display = "none");
            break;
        case "top":
            start
                ? (topImg.style.display = "block")
                : (topImg.style.display = "none");
            break;
        case "bottom":
            start
                ? (bottomImg.style.display = "block")
                : (bottomImg.style.display = "none");
            break;
        default:
            break;
    }
}

// "template" function for the mouse tracker event listener callbacks
function trackHelper(event, imgObject) {
    let width = window.getComputedStyle(imgObject).width;
    imgObject.style.top = `calc(${event.y}px - calc(${width} / 2))`;
    imgObject.style.left = `calc(${event.x}px - calc(${width} / 2))`;
}

// create named callback for every image
const trackLeft = (event) => {
    trackHelper(event, leftImg);
};
const trackRight = (event) => {
    trackHelper(event, rightImg);
};
const trackTop = (event) => {
    trackHelper(event, topImg);
};
const trackBottom = (event) => {
    trackHelper(event, bottomImg);
};

// if start=true, track, if false, stop tracking
//? maybe combine this with show into one function?
function track(dir, start = true) {
    switch (dir) {
        case "left":
            start
                ? window.addEventListener("mousemove", trackLeft)
                : window.removeEventListener("mousemove", trackLeft);
            break;
        case "right":
            start
                ? window.addEventListener("mousemove", trackRight)
                : window.removeEventListener("mousemove", trackRight);
            break;
        case "top":
            start
                ? window.addEventListener("mousemove", trackTop)
                : window.removeEventListener("mousemove", trackTop);
            break;
        case "bottom":
            start
                ? window.addEventListener("mousemove", trackBottom)
                : window.removeEventListener("mousemove", trackBottom);
            break;

        default:
            break;
    }
}

// check if a point is inside an element
// needed to un-display an images on a route event when no present exists anymore at that place
function isPointInsideElement(x, y, element) {
    // Get the bounding rectangle of the element
    const rect = element.getBoundingClientRect();

    // Check if the point is within the bounding rectangle
    return (
        x >= rect.left && x <= rect.right && y >= rect.top && y <= rect.bottom
    );
}

// start looking for mutations
observer.observe(appContainer, config);
