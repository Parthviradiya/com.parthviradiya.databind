# Data Binding For Unity: 

- One-Way MVVM(model view view model) style data-binding for Unity.

- Data Bind is a simple data binding system which enables one-way data binding from observable model data to view components.

# Installation
- Install package from PackageManager by this GitUrl: https://github.com/Parthviradiya/com.parthviradiya.databind.git
- You can also install package samples from package manager window.

# Getting Started
- Explore the MyExample scene in the package sample folder.

- The scene contains a View Model game object with a ViewModel component And also ViewModel scriptable object with ViewModelScriptable component Change the values on the component to observe changes in the scene.

- The scene contains a few sample bindings on single game objects as well as a slightly more complex list example which binds prefab instances to a list of configuration data.

# Usage
-First define a new class which extends ViewModel or ViewModelScriptable. ViewModels are Unity MonoBehaviours and ViewModelScriptable are Unity ScriptableObject which should contain several DataSource or DataSourceList fields.

# Example:
- You can create both the monobehaviour and scriptableobject viewmodels.

```javascript
1. MonoBehavior Example:
public class ItemViewModel : ViewModel
{
	public SpriteDataSource ItemImage;
	public StringDataSource Itemname;
	public IntDataSource itemPrice;
}

2. ScriptableObject Example:
public class ItemViewModel : ViewModelScriptable
{
  	public SpriteDataSource ItemImage;
  	public StringDataSource Itemname;
	public IntDataSource itemPrice;
}

//At runtime, data source values can be accessed via the Value property.

ViewModel viewModel = gameObject.GetComponent<ItemViewModel>();
viewModel.ItemImage.Value = someSprite;
viewModel.Itemname.Value = “Chocolate”;
viewModel.itemPrice.Value = 55;

```

## 🔗 Links
[![portfolio](https://img.shields.io/badge/my_portfolio-000?style=for-the-badge&logo=ko-fi&logoColor=white)](https://parthviradiya.github.io/)

