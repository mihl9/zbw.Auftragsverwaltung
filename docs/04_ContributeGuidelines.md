# Guidelines

We use for our development and branching Workflow the Git-Flow Strategy. With the slight difference that in our current state of development we do not have a Release Branch.
Our Main Branch ist currently de Development branch till the Initial devlopment is concluded.

## Git Flow
![GitFlow](https://blog.seibert-media.net/wp-content/uploads/2014/03/Gitflow-Workflow-2.png)

## Branch Naming

Currently there are 3 possible types of Branches:
    
- Feature   -> feat
- Fix       -> fix
- Doc       -> doc

these Types are always branched from the development-branch. And must always named like the following example

    {Type}/{Number}           -> Example
    
    feat/{TrelloTaskNumber}     -> feat/#009.01
    
    fix/{TrelloTaskNumber}      -> fix/#B009
    
    doc/{TrelloTaskNumber}      -> doc/#010.1

## Commits

Each commit must comply with these specifications:

- Use the present tense ("Add feature" not "Added feature")
- Use the imperative mood ("Move cursor to..." not "Moves cursor to...")
- Limit the Title line to 72 characters or less
- Reference issues and pull requests liberally after the first line
- When only changing documentation, include [ci skip] in the commit title
- Consider starting the commit message with an applicable shortcut:
    * feat: when adding new features
    * fix:  when fixing a bug
    * doc:  when writing documentation
    * refac: when refactoring code or project structure

## Push Requests


## Creating a Issue

    To be written