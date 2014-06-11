# Code Structure

* BaseComponent defines scripts built to placed on objects.
* Data defines classes that are serializable.

## Notes

* All Components should inherit BaseComponent, NOT Monobehavior
* All Non-Monobehavior Scripts should either inherit the Data class if they contain no business logic, or the entity class for business logic.