# Structure of Slides.json

## Example structure

```json
{
    "SlideId": {
        "Tag": ["tag name", "another tag"],
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
        <td></td>
    </tr>
    <tr>
        <td><code>Image</code></td>
        <td>
            The path to the image file that will be displayed as the background of the slide.
        </td>
        <td></td>
    </tr>
    <tr>
        <td><code>Buttons</code></td>
        <td>
            A dictionary containing the data for all the buttons on the slide.
        </td>
        <td></td>
    </tr>
    <!--  -->
    <tr>
        <td align=center colspan=3 ><strong>Button Level</strong></th>
    </tr>
    <tr>
        <td><code>*Tag</code></td>
        <td>
            List of strings cotaining tags to mark a button. Pretty much the same as the <code>Tag</code> in the slide object.
        </td>
        <td></td>
    </tr>
    <tr>
        <td><code>Type</code></td>
        <td>
            Specifies what kind of button it is. Can be <code>polygon</code>, <code>rect</code>, <code>circle</code>, <code>image</code>, <code>*ellipse</code>
        </td>
        <td></td>
    </tr>
    <tr>
        <td><code>Points</code></td>
        <td>
            The data for the shape. What kind of data this is depends on the <code>Type</code> of the button. Explained in detail in the [Points section](#points)
        </td>
        <td></td>
    </tr>
    <tr>
        <td><code>Image</code></td>
        <td>
            The path to the image file that will be displayed as the button. Only needed it the <code>Type</code> is <code>image</code>.
        </td>
        <td></td>
    </tr>
    <tr>
        <td><code>Actions</code></td>
        <td>
            A list of lists. Each list contains the data for one action that is executed when the button is clicked. Explained in detail in the [Actions section](#actions).
        </td>
        <td></td>
    </tr>
    <tr>
        <td><code>*Visible</code></td>
        <td>
            a
        </td>
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
    </tr>
    <tr>
        <td>
            <code>*PlaySound</code>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*AddItem</code>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*RemoveItem</code>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*RequireItem</code>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*RequireGameState</code>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*SetGameState</code>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*</code>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <code>*InventoryRemove</code>
        </td>
        <td></td>
        <td></td>
    </tr>

</table>

### Naming Conventions
