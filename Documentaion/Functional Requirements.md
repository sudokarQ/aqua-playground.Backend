# From the user's side
1. When entering the site, the system should display to the user the main page containing a list of links to menu categories, buttons “Entrance”, “Registration" and the phone number of the institution in international format
2. When you click on the “Register” button, the system should display a graphical window with the text field “Phone Number”, the buttons “Register”, “Close".
	1. The “Phone number” field must be filled in before registration.
	2. After clicking on the “Register” button, the system should display the main page of the site (item 1) and send an SMS with a password to the specified phone
	3. After clicking on the “Register” button, the system should display the main page of the site (item 1) and send an SMS with a password to the specified phone
	4. After clicking on the “Close” button, the system displays the main page of the site (item 1)
3. When you click on the “Login” the system should display a graphical window with text fields “Phone Number”, “Password”, buttons “Log in”, “Close”
	1. Both text fields (item 3) must be filled in
	2. After clicking on the “Close” button, the system displays the main page of the site (item 1)
	3. When clicking on the “Log in” button, the system must verify the entered data with the data during registration
		1. If the data match, the system displays the main page of the site (item 1). The user has logged in to the account.
		2. If the data does not match, the system displays a text notification “Incorrect phone number or password”
4. When clicking on the menu category link (item 1), the system should display all menu items in this category in the form of a tile.
5. Each item must contain a photo, name, composition and cost
6. When you click on a position, a graphical notification about adding a position to the cart should appear.
7. When using the “Shopping Cart” button, the system should display a list of items that the user added to the cart, a check box for choosing a payment method, a text field for entering an address, a text field for specifying a comment to the order and the “checkout” button.
8. Each item in the basket (item 5) must contain the name, image, quantity and total cost.
9. The total cost of the item (item 6) is calculated by multiplying the quantity of the product multiplied by its price after taking into account all discounts and promotions.
10. Each item in the list (item 5) should have two buttons “+” and “-"
11. When the LMB is on “+”, the system should increase the quantity of goods by 1, generate a new total cost and display it (item 7)
12. When the LMB is on “-”, the system should reduce the quantity 1, generate a new total cost and display it (item 7)
13. When reducing the quantity of goods to 0 (item10), the system must remove the goods from the basket (item 5)
14. The payment method selection check box (item 5) must contain two items: “Cash payment” and “Card payment upon receipt”
15. The initial position for the check-box (item 12) should be the “Cash payment” option
16. The text field for entering the address (item 5) must be filled in before sending the order, the text field for comments is not required.
17. When clicking on the “Place an order” button, the system should show the user a notification of successful order processing, display the order identification number.

# From the Administrator's side
1. Order Management:
	1. The site should display all orders in the form of a list containing all the parameters of the entity when the “Orders” button is clicked
	2. When you left-click on a line from the list of orders, the system should display a context menu with the following items:
		• Changing the order status
		• Adding a comment to the order
		• Order deletion
	3. When hovering the cursor over the item “Order status change” in the context menu (clause 1.1), the system should display a drop-down menu with a list of statuses (Accepted, Preparing, Transiting, Delivery, Closed, Cancelled)
	4. The administrator can change the status of the order by clicking on the drop-down menu item (clause 1.3).
	5. When clicking on the item “Adding a comment to the order” in the context menu (clause 1.1), the system must display a text input window.
	6. If there is a comment to the order, it must be displayed in the text input window (clause 1.5)
	7. The administrator can add a comment to the order by entering text in the text input window (clause 1.5)
	8. The system must delete the order from the list of orders (clause 1.1) when clicking on the “Delete order" item (clause 1.2)
	9. When you click on the “Export to Excel” button, the system should generate and start downloading the file.xlsx containing full order lists (clause 1.1)
2. Menu management:
	1. The site should display all menu items in the form of a list containing all the entity parameters when the “Menu” button is clicked
	2. The “Add” button should be located above the list (clause 2.1)
	3. The system should display a context menu when the right mouse button is clicked on the menu line (clause 2.1) with the position
	4. The context menu (clause 3.1) must contain the “Delete”, “Edit” buttons
	5. When clicking the LMB on the delete item (clause 2.4), the system must delete the item from the list (clause 2.1)
	6. When clicking the LMB on the edit item (clause 2.4), the system should display a graphical window in which editing of all position parameters from the menu is available (clause 2.1)
	7. The graphical window (clause 2.6) must contain in the fields of the order parameters its current values, available for editing
	8. The graphic window (item 2.6) should have the "Save” and “Close" buttons.
	9. When the LMB is pressed on the “Save” button (p.2.8), the system displays a list of positions (p.2.1) with updated data specified in the graphical window (p.2.6)
	10. When the LMB is pressed on the “Close” button (p.2.8), the system displays a list of positions (p.2.1) not accepting the changes indicated in the graphical window (clause 2.6)
3. Promotion management:
	1. The site should display all the Promotions of the institution in the form of a list containing all the parameters of the entity when the “Promotions” button is clicked
	2. The “Add” button should be located above the list (clause 3.1)
	3. The system should display a context menu when the right mouse button is clicked on the menu line (clause 3.1) with the action
	4. The context menu (clause 3.1) must contain the “Delete”, “Edit” buttons
	5. When clicking the LMB on the delete item (clause 3.4), the system must delete the item from the list (clause 3.1)
	6. When clicking the LMB on the edit item (clause 3.4), the system should display a graphical window in which editing of all position parameters from promotions is available (clause 3.1)
	7. The graphical window (clause 3.6) must contain in the fields of the order parameters its current values, available for editing
	8. The graphic window (item 3.6) should have the "Save” and “Close" buttons.
	9. When the LMB is pressed on the “Save” button (clause 3.8), the system displays a list of Promotions (clause 3.1) with updated data specified in the graphical window (clause3.6)
	10. When the LMB is pressed on the “Close” button (clause3.8), the system displays a list of Promotions (clause 3.1).3.1) not accepting the changes specified in the graphical window (clause3.6)
4. User management:
	1. The site should display all users of the site in the form of a list containing all the parameters of the entity when the “Users” button is clicked
	2. The “Add” button should be located above the list (clause 4.1)
	3. The system should display a context menu when the right mouse button is clicked on the menu line (clause 4.1) with the action
	4. The context menu (clause 4.1) must contain the “Delete”, “Edit” buttons
	5. When clicking the LMB on the delete item (clause 4.4), the system must delete the item from the list (clause 4.1)
	6. When clicking the LMB on the edit item (clause 4.4), the system should display a graphical window in which editing of all user parameters is available (clause 4.1)
	7. The graphical window (clause 4.6) must contain in the fields of the order parameters its current values, available for editing
	8. The graphic window (clause 4.6) should have the "Save” and “Close" buttons.
	9. When the LMB is pressed on the “Save” button (clause 4.8), the system displays a list of Promotions (clause 4.1) with updated data specified in the graphical window (clause4.6)
	10. When the LMB is pressed on the “Close” button (clause4.8), the system displays a list of users (clause4.1) not accepting the changes indicated in the graphical window (clause 4.6)