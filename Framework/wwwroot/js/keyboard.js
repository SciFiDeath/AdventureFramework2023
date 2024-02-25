// found here: https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-dotnet-from-javascript?view=aspnetcore-6.0#pass-a-dotnetobjectreference-to-a-class-with-multiple-javascript-functions
// I have no idea how classes work in js
// probably cause nobody uses them
// class Keyboard {
//     static keyboardServiceRef;

//     static setKeyboardServiceRef(ref) {
//         Keyboard.keyboardServiceRef = ref;
//     }
// }

window.keyboard = {
    objRef: null,
    init: init,
};

function init(objRef) {
    window.keyboard.objRef = objRef;
    window.addEventListener("keydown", keydown);
    window.addEventListener("keyup", keyup);
}

function keydown(event) {
    window.keyboard.objRef.invokeMethodAsync("KeyDown", event.code);
}

function keyup(event) {
    window.keyboard.objRef.invokeMethodAsync("KeyUp", event.code);
}
