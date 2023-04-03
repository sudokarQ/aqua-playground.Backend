# User Journey
## Client
<ol>
<li>User enters the website and sees the main page with links to menu categories, the "Login" button, the "Registration" button, and the phone number of the institution in international format.</li>
<li>User clicks the "Registration" button and sees a graphical window with a "Phone Number" field, a "Register" button, and a "Close" button.</li>
<li>User enters their phone number in the "Phone Number" field and clicks the "Register" button.</li>
<li>The system sends an SMS with a password to the specified phone and displays the main page of the site.</li>
<li>User clicks the "Login" button and sees a graphical window with "Phone Number" and "Password" fields, a "Log in" button, and a "Close" button.</li>
<li>User enters their phone number and password in the respective fields and clicks the "Log in" button.</li>
<li>The system verifies the entered data with the data during registration. If the data matches, the system displays the main page of the site and the user is logged in to their account. If the data does not match, the system displays a text notification "Incorrect phone number or password".</li>
<li>User clicks on a menu category link and sees all menu items in this category in the form of a tile.</li>
<li>User clicks on a position and sees a graphical notification about adding a position to the cart.</li>
<li>User adds more items to the cart if desired.</li>
<li>User clicks the "Shopping Cart" button and sees a list of items added to the cart, a check box for choosing a payment method, a text field for entering an address, a text field for specifying a comment to the order, and a "checkout" button.</li>
<li>User selects a payment method, enters their address in the text field, adds a comment if desired, and clicks the "Place an order" button.</li>
<li>The system shows the user a notification of successful order processing, displays the order identification number, and the user receives an email confirmation of the order.</li>
<li>User logs out of their account and leaves the site.</li>
</ol>

## Admin 
#### Order Management
<ol>
<li>When the admin clicks on the "Orders" button, the site displays a list of all orders containing all the parameters of the entity.</li>
<li>When the admin left-clicks on a line from the list of orders, the system displays a context menu with the following items:
<ul>
	<li>Change the order status</li>
	<li>Add a comment to the order</li>
	<li>Delete the order</li>
</ul>
<li>When hovering the cursor over the "Change the order status" item in the context menu, the system displays a drop-down menu with a list of statuses (Accepted, Preparing, Transiting, Delivery, Closed, Cancelled).</li>
<li>The admin can change the status of the order by clicking on the desired status in the drop-down menu.</li>
<li>When clicking on the "Add a comment to the order" item in the context menu, the system displays a text input window.</li>
<li>If there is already a comment on the order, it is displayed in the text input window.</li>
<li>The admin can add a comment to the order by entering text in the text input window.</li>
<li>When the admin clicks on the "Delete order" item in the context menu, the system deletes the order from the list of orders.</li>
<li>When the admin clicks on the "Export to Excel" button, the system generates and starts downloading a file.xlsx containing a full list of orders.</li>
</ol>

#### Menu Management
<ol>
<li>When the admin clicks on the "Menu" button, the site displays a list of all menu items containing all the entity parameters.</li>
<li>The "Add" button is located above the list.</li>
<li>When the admin right-clicks on a menu line, the system displays a context menu with the following items:</li>
<ul>
<li>Delete the menu item</li>
<li>Edit the menu item</li>
</ul>
<li>When the admin left-clicks on the "Delete" item in the context menu, the system deletes the menu item from the list.</li>
<li>When the admin left-clicks on the "Edit" item in the context menu, the system displays a graphical window in which all position parameters from the menu are available for editing.</li>
<li>The graphical window contains fields for the current values of the order parameters, which are available for editing.</li>
<li>The graphical window has "Save" and "Close" buttons.</li>
<li>When the admin left-clicks on the "Save" button, the system displays a list of positions with the updated data specified in the graphical window.</li>
<li>When the admin left-clicks on the "Close" button, the system displays a list of positions that do not accept the changes specified in the graphical window.</li>
</ol>

#### Promotion Management
<ol>
<li>When the admin clicks on the "Promotions" button, the site displays a list of all promotions containing all the parameters of the entity.
<li>The "Add" button is located above the list.
<li>When the admin right-clicks on a promotion line, the system displays a context menu with the following items:
<ol>
<li>Delete the promotion
<li>Edit the promotion
</ol>
<li>When the admin left-clicks on the "Delete" item in the context menu, the system deletes the promotion from the list.
<li>When the admin left-clicks on the "Edit" item in the context menu, the system displays a graphical window in which all position parameters from the promotion are available for editing.
<li>The graphical window contains fields for the current values of the order parameters, which are available for editing.
<li>The graphical window has "Save" and "Close" buttons.
<li>When the admin left-clicks on the "Save" button,the system displays a list of promotions with the updated data specified in the graphical window.</li>
<li>When the admin left-clicks on the "Close" button, the system displays a list of promotions that do not accept the changes specified in the graphical window.</li>
</ol>

#### User Management
<ol>
<li>When the admin clicks on the "Users" button, the site displays a list of all users containing all the parameters of the entity.
<li>The "Add" button is located above the list.
<li>When the admin right-clicks on a promotion line, the system displays a context menu with the following items:
<ol>
<li>Delete the user
<li>Edit the user
</ol>
<li>When the admin left-clicks on the "Delete" item in the context menu, the system deletes the user from the list.
<li>When the admin left-clicks on the "Edit" item in the context menu, the system displays a graphical window in which all position parameters from the user are available for editing.
<li>The graphical window contains fields for the current values of the order parameters, which are available for editing.
<li>The graphical window has "Save" and "Close" buttons.
<li>When the admin left-clicks on the "Save" button,the system displays a list of users with the updated data specified in the graphical window.</li>
<li>When the admin left-clicks on the "Close" button, the system displays a list of users that do not accept the changes specified in the graphical window.</li>
</ol>
