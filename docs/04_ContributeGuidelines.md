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
	* feat: A new feature
    * fix: A bug fix
    * docs: Documentation only changes
    * style: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
    * refactor: A code change that neither fixes a bug nor adds a feature
    * perf: A code change that improves performance
    * test: Adding missing or correcting existing tests
    * chore: Changes to the build process or auxiliary tools and libraries such as documentation generation

## Push Requests


## Creating a Issue

    To be written