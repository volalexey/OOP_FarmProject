# Course Project: Farm Management System

## Overview
The **Farm Management System** is a comprehensive Object-Oriented Programming (OOP) solution designed to model the economic and logistical operations of a modern agricultural enterprise. The application simulates the management of land resources, personnel, and financial assets using advanced C# architectural patterns.

## Domain Model
The system is built around a central **Farm** entity that acts as a composite manager for various resources:

* **Land Management:** The farm owns multiple **Plots** (Composition relationship). These are abstract entities specialized into:
    * **CropPlots:** Dedicated to agriculture (Wheat, Corn, Potato).
    * **AnimalPens:** Dedicated to livestock (Cows, Chickens, Pigs).
* **Human Resources:** The farm employs a **Manager** and multiple **Workers** (Aggregation relationship).
    * Implemented via the `IPerson` interface to allow polymorphic handling of all staff.
* **Economics:** Each plot calculates income differently based on its type (polymorphism). Workers act as efficiency multipliers, increasing the output of the plots they are assigned to.

## Technical Architecture

### Core OOP Principles
* **Inheritance & Abstraction:** Utilizes an abstract base class `Plot` to define shared logic (cost, size, base income) while deriving specialized classes for specific behaviors.
* **Polymorphism:** Implements virtual methods for income calculation and interface-based polymorphism (`IPerson`, `IProducible`) to treat diverse objects uniformly.
* **Relationships:** Strictly defined UML relationships including Association, Aggregation, and Composition.

### Advanced .NET Features
The project integrates standard .NET interfaces to enhance collection management:
* **Sorting (`IComparable<T>`, `IComparer<T>`):** Enables sorting of Workers by efficiency and Plots by profitability.
* **Iteration (`IEnumerable<T>`):** Implements custom iterators to traverse complex farm structures using standard `foreach` loops.
* **Cloning (`ICloneable`):** Supports deep and shallow copying of complex objects to manage state duplication.

## Functional Requirements
The application features an interactive console interface allowing the user to:
1.  **Manage Assets:** Buy new land plots and upgrade existing facilities.
2.  **HR Management:** Hire and fire workers; assign staff to specific plots to boost production.
3.  **Financials:** Collect income from all sources and track the global farm balance.
4.  **Reporting:** View sorted lists of most productive workers and profitable lands.

## Quality Assurance
* **Unit Testing:** The solution includes a comprehensive MSTest project verifying the structural integrity, business logic, and mathematical accuracy of the simulation.
