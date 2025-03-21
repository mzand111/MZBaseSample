# MZBaseSample
This implementation serves as a sample company registration service, leveraging [MZBase](https://github.com/mzand111/MZBase) to incorporate repository, Unit of Work (UoW), and service design patterns. The solution provides an API to perform CRUD operations (Create, Read, Update, Delete) on a sample entity: `Company`. The `Company` model includes a collection of branches (`CompanyBranch`), which are seamlessly managed and saved alongside their parent Company entity.
This is the UML diagram of the domain models.
![Diagram](https://github.com/user-attachments/assets/062f2489-f63f-42e6-b060-f3129f0e34ab)

While this sample is quite simple and the use of these patterns may not be strictly necessary for a context of this complexity, exploring this repository can provide valuable insights into utilizing the MZBase library's base classes effectively.

## ToDo
The next step involves introducing a set of automated tests to demonstrate the use of the MZBase.Test library in facilitating automated test generation at the service level.
