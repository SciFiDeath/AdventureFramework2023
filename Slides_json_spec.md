# Structure of Slides.json

## Example structure

```json
{
    "SlideId": {
        "Tags": ["tag name", "another tag"],
        "Image": "path/to/image",
        "Buttons": {
            "Button1Id": {
                "Tag": "tag name",
                "Type": "polygon/rect/circle/image/...",
                "Points": "data for the shape",
                "Actions": [
                    ["action type", "data for", "the action"],
                    ...
                ]
            },
            ...
        }
    },
    ...
}
```

## Explanation in Detail

> Stuff marked with a \* is not yet implemented.

<table>
    <tr>
        <th>Field</th>
        <th>Explanation</th>
        <th>Notes</th>
    </tr>
    <!--  -->
    <tr>
        <td align=center colspan=3 ><strong>File Level</strong></th>
    </tr>
    <tr>
        <td><code>SlideId</code></td>
        <td>
            A unique identifier for the slide. Must be unique across all slides.
        </td>
        <td></td>
    </tr>
    <!--  -->
    <tr>
        <td align=center colspan=3><strong>Slide Level</strong></th>
    </tr>
    <tr>
        <td><code>*Tags</code></td>
        <td>
            Array of strings. Tags can be used to mark a slide. Basically a dodgy solution to implement more functionality without changing the structure of the json too much.
        </td>
        <td>Optional</td>
    </tr>
    <tr>
        <td><code>Image</code></td>
        <td>
            The path to the image file that will be displayed as the background of the slide.
        </td>
        <td>Required</td>
    </tr>
    <tr>
        <td><code>Buttons</code></td>
        <td>
            A dictionary containing the data for all the buttons on the slide.
        </td>
        <td>Required. If a Slide should have no buttons (which should never be the case as you would be trapped there), just set it as an empty object</td>
    </tr>
    <!--  -->
    <tr>
        <td align=center colspan=3 ><strong>Button Level</strong></th>
    </tr>
    <tr>
        <td><code>*Tags</code></td>
        <td>
            List of strings cotaining tags to mark a button. Pretty much the same as the <code>Tag</code> in the slide object.
        </td>
        <td>Optional</td>
    </tr>
    <tr>
        <td><code>Type</code></td>
        <td>
            Specifies what kind of button it is. Can be <code>polygon</code>, <code>rect</code>, <code>circle</code>, <code>image</code>, <code>*ellipse</code>
        </td>
        <td>Required</td>
    </tr>
    <tr>
        <td><code>Points</code></td>
        <td>
            The data for the shape. What kind of data this is depends on the <code>Type</code> of the button. Explained in detail in the <a href=#points>Points section</a>.
        </td>
        <td>Required</td>
    </tr>
    <tr>
        <td><code>Image</code></td>
        <td>
            The path to the image file that will be displayed as the button.
        </td>
        <td>Required if <code>Type</code> is <code>image</code>. Will cause exception if specified when <code>Type</code> is something else.</td>
    </tr>
    <tr>
        <td><code>Actions</code></td>
        <td>
            A list of lists. Each list contains the data for one action that is executed when the button is clicked. Explained in detail in the <a href=#actions>Actions section</a>.
        </td>
        <td>Required. If a button should have no actions, set it to an empty array. (maybe make it nullable?)</td>
    </tr>
    <tr>
        <td><code>*Visible</code></td>
        <td>
            Controls visibility of the Slide. If it is set, a GameState entry is generated automatically. Every generated entry is set to <code>true</code>.
            <ul>
                <li>If set to <code>auto</code>, the GameState key will be automatically generated.</li>
                <li>If set to <code>&lt;custom key name&gt;</code>, you can specify the GameState key manually. Make sure to follow the <a href=#naming-conventions>naming conventions</a></li>
                <li>If you want the slide to be hidden at the start, add a <code>!</code> in front of the name.</li>
            </ul>
        </td>
        <td>
            <code>"Visible": "!auto"</code> will autogenerate a GameState entry, and the slide will be hidden at the start. <br>
            <code>"Visible": "auto"</code> will autogenerate a GameState entry, and the slide will be visible at the start. <br>
            <code>"Visible": "custom key name"</code> will generate a GameState entry with the specified key, and the slide will be visible at the start. <br>
            <code>"Visible": "!custom key name"</code> will generate a GameState entry with the specified key, and the slide will be hidden at the start.
        </td>
    </tr>
    <tr>
        <td><code>*ZIndex</code></td>
        <td>
            The z-index of the button. The higher the number, the more on top it is. If not specified, the order of the buttons in the json file is used (or whatever order it is when you iterate over it in a foreach-loop).
        </td>
        <td>Optional</td>
    </tr>

</table>

<!-- ### File level

The entire file is basically a big dictionary, where the keys are the `SlideId` and the values are objects containing the data for the slide of said Id.

-   `SlideId`: A unique identifier for the slide. Must be unique across all slides.

### Slide-level

A slide object contains all the data neccessary to display one slide.

-   `Tag`: A tag that can be used to mark a slide. Can contain multiple tags seperated by spaces/commas(need to decide on that). Basically a dodgy solution to implement more functionality without changing too much.
-   `Image`: The path to the image file that will be displayed as the background of the slide.
-   `Buttons`: A dictionary containing the data for all the buttons on the slide.

### Button level

A button object is not only a button. It is an object that is rendered over the background image and can (most often) be clicked. It is either an SVG shape (polygon, rectangle, circle as of now) or an image.

-   [\*]`Tag`: A tag that can be used to mark a button. Can contain multiple tags seperated by spaces/commas(need to decide on that). Pretty much the same as the `Tag` in the slide object.
-   `Type`: Specifies what kind of button it is. Can be `polygon`, `rect`, `circle`, `image` as of now.
-   `Points`: The data for the shape. What kind of data this is depends on the `Type` of the button. Explained in detail in the next section.
-   `Actions`: A list of lists. Each list contains the data for one action that is executed when the button is clicked. More information follows. -->

#### Points

Based on the `Type` of the button, the `Points` field needs to contain different data. The following is a list of the different `Type`s and what kind of data the `Points` field needs to contain for each of them.

<!--
-   `polygon`: A list of points that define the vertecies of the polygon. The points are ordered in a way that the polygon is drawn. The list is space seperated, where the corresponding x and y coordinates are seperated by a comma. Example: `0,0 100,0 100,100 0,100`. [More Information](https://www.w3schools.com/graphics/svg_polygon.asp)
-   `rect`: A comma seperated list of 4 values. The first two are the x and y coordinates of the top left corner of the rectangle, the second two are the width and height of the rectangle. Example: `0,0,100,100`. [More Information](https://www.w3schools.com/graphics/svg_rect.asp)
-   `circle`: A comma seperated list of 3 values. The first two are the x and y coordinates of the center of the circle, the third is the radius of the circle. Example: `50,50,50`. [More Information](https://www.w3schools.com/graphics/svg_circle.asp)
-   `image`: A comma seperated list of 4 (optional) values. The first two are the x and y coordinates of the top left corner of the image, the second two are the width and height of the image. None of the values are required, but you should at least specify either width or height. x and y default to 0 if not specified. If you want to not specify a value, just leave it empty. Example: `, , 100,`. Here, only width is specified. Spaces are not needed, but make it more readable. [More Information](https://developer.mozilla.org/en-US/docs/Web/SVG/Element/image) -->

<table>
    <tr>
        <th>Shape type</th>
        <th>Points</th>
        <th>Examples</th>
    </tr>
    <tr>
        <td><code>polygon</code></td>
        <td>
            A space seperated list of vertecies that define the polygon. The x and y coordinates are seperated by a comma. <a href="https://www.w3schools.com/graphics/svg_polygon.asp">More Information</a>
        </td>
        <td>
            <code>0,0 100,0 100,100 0,100</code>
        </td>
    </tr>
    <tr>
        <td><code>rect</code></td>
        <td>
            A comma seperated string of 4 values. The first two are the x and y coordinates of the top left corner of the rectangle, the second two are the width and height of the rectangle. <a href="https://www.w3schools.com/graphics/svg_rect.asp">More Information</a>
        </td>
        <td>
            <code>0,0,100,100</code>
        </td>
    </tr>
    <tr>
        <td>
            <code>circle</code>
        </td>
        <td>
            A comma seperated string of 3 values. The first two are the x and y coordinates of the center of the circle, the third is the radius of the circle. <a href="https://www.w3schools.com/graphics/svg_circle.asp">More Information</a>
        </td>
        <td>
            <code>50,50,50</code>
        </td>
    </tr>
    <tr>
        <td>
            <code>image</code>
        </td>
        <td>
            A comma seperated string of 4 (optional) values. The first two are the x and y coordinates of the top left corner of the image, the second two are the width and height of the image. Technicaly, none of the values are required, but you should at least specify either width or height, or there will be problems with scaling. x and y default to 0 if not specified. If you want to not specify a value, just leave it empty. <a href="https://developer.mozilla.org/en-US/docs/Web/SVG/Element/image">More Information</a>
        </td>
        <td>
            <code>,,100,</code> Here, only width is specified.
        </td>
    </tr>
    <tr>
        <td>
            <code>*ellipse</code>
        </td>
        <td>
            A comma seperated string of 4 values. The first two are the x and y coordinates of the center of the ellipse, the second two are the radius of the ellipse. <a href="https://www.w3schools.com/graphics/svg_ellipse.asp">More Information</a>
        </td>
        <td>
            <code>50,50,50,25</code>
        </td>
</table>

### Actions

Actions are quite a convoluted mess.

<table>
    <tr>
        <th>Action Type</th>
        <th>
            Explanation
        </th>
        <th>
            Parameters
        </th>
        <th>
            Example
        </th>
    </tr>
    <tr>
        <td>
            <code>Route</code>
        </td>
        <td>
            Changes the slide to the one specified in the parameters.
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*PlaySound</code>
        </td>
        <td>Plays a sound</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*AddItem</code>
        </td>
        <td>Add the item with the specified id to the inventory</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*RemoveItem</code>
        </td>
        <td>Remove the item with the specified id from the inventory</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*SetGameState</code>
        </td>
        <td>Set the value of a GameState entry</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*RequireItem</code>
        </td>
        <td>Checks if item with specified id is present in the inventory. To check if an item is not present, add a <code>!</code> in front of the id. If true, it proceeds with executing the rest of the actions. If false, it exits.</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*RequireGameState</code>
        </td>
        <td>Checks if the specified GameState is true. To check if  a GameState entry is false, add a <code>!</code> in front of the key. If true, it proceeds with executing the rest of the actions, if false, it exits.</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*StartBlock</code>
        </td>
        <td>If this action is followed by a <code>Require</code> action, it checks if said Require evals to true, it executes the following code block. If it is false, it jumps to the corresponding <code>EndBlock</code> action.</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*EndBlock</code>
        </td>
        <td>Denotes the end of a block started with <code>StartBlock</code>. Every <code>StartBlock</code> must have a corresponding <code>EndBlock</code> action. Else it will raise an error</td>
        <td></td>
        <td></td>
    </tr>
</table>

### Naming Conventions

<table>
    <tr>
        <th>Field</th>
        <th>Description</th>
        <th>Example</th>
    </tr>
    <tr>
        <td><code>SlideId</code></td>
        <td>
            The <code>SlideId</code> must be unique across all slides. It should first contain the name of the group (e.g. the room) and then a name for the slide. The two parts are seperated by a dot. The name of the group should be the same as the name of the folder the images for the slides are in. Ids and rooms should be in snake_case/PascalCase (need to decide on that).
        </td>
        <td>
            <code>HM305.BlackboardCloseup</code>
            <code>hm305.blackboard_closeup</code>
        </td>
    </tr>
    <tr>
        <td><code>ButtonId</code></td>
        <td>
            The <code>ButtonId</code> must be unique across all buttons on a slide. It doesn't have to contain the name of the slide. Naming in either snake_case/PascalCase (need to decide on that).
        </td>
        <td>
            <code>ExitDoor</code>
            <code>exit_door</code>
        </td>
    </tr>
    <tr>
        <td><code>GameState</code></td>
        <td>
            The <code>GameState</code> key must be unique. For buttons, you concatenate the <code>SlideId</code> and the <code>ButtonId</code> with a dot. For Minigames, you take the name of the minigame.
        </td>
        <td>
            <code>HM305.BlackboardCloseup.ExitDoor</code>
            <code>hm305.blackboard_closeup.exit_door</code>
        </td>
    </tr>
    <tr>
        <td>Minigames</td>
        <td>Minigames should be named like the following:<br><code>&lt;SlideId&gt;.&lt;Minigame name&gt;.minigame</code></td>
        <td>
            <code>HM305.BlackboardCloseup.CodeTerminal.Minigame</code>
            <code>hm305.blackboard_closeup.code_terminal.minigame</code>
        </td>
    </tr>
</table>

# Folder Structure

```
Pages
    Index.razor
Shared
    Minigame
        <Minigame stuff>
    Slides
        <Slide stuff>
    Services
        <SlideService, GameState, Mouse- and KeyboardService, JsonUtility etc.>
wwwroot
    css
        <autogenerated css, don't touch>
    js
        <javascript stuff, only framework crew allowed>
    images
        <example:>
        hm305
            blackboard
                <background image>
                bg.png
                <button images>
                exit_door.png
            blackboard_closeup
                <background image>
                bg.png
            blackboard_closeup.minigame
                <images and stuff, name like you want I guess>

        <and so on>

_Imports.razor
App.razor
Framework.csproj
MainLayout.razor
MainLayout.razor.css
Program.cs
```
