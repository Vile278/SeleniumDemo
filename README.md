# Selenium C# Demo

## 1. Run tests

dotnet test

dotnet test --filter "Category=Register4"

dotnet test --filter "Name=Verify_Country_Names"

## 2. Push code to Github
2.1 Verify the Project Before Pushing

Run the following commands to ensure the project builds and all tests pass successfully:

dotnet clean
dotnet restore
dotnet test

Expected result:

Build succeeded
Test summary: total: 5, failed: 0

If all tests pass, the project is ready to be pushed to GitHub.

2.2 Initialize Git Repository
git init
2.3 Stage All Files
git add .

This command adds all project files to the Git staging area.

2.4 Create the First Commit
git commit -m "Initial Selenium NUnit project"

This command saves the staged changes to the local Git repository.

2.5 Rename the Default Branch to Main
git branch -M main
2.6 Connect the Local Repository to GitHub
git remote add origin https://github.com/Vile278/SeleniumDemo.git

This command links the local repository to the remote GitHub repository.

2.7 Push Code to GitHub
git push -u origin main

This command pushes the local code to the remote repository and sets the upstream branch for future pushes.

2.8 Verify the Repository

Open the GitHub repository in your browser and verify that:

All source code files are uploaded.
The README.md file is displayed correctly.
Unnecessary files such as bin/, obj/, and .vs/ are excluded by .gitignore.

## 3. Run test automatically with GitHub Actions:

3.1 Create the Workflow Directory

Create the following folder structure in your project:

.github
└── workflows
    └── dotnet.yml
3.2 Create the Workflow File

Create a file named dotnet.yml inside the .github/workflows folder and add the following content:

name: Selenium NUnit Tests

on:
  push:
    branches:
      - main

  pull_request:
    branches:
      - main

jobs:
  test:

    runs-on: ubuntu-latest

    steps:

    - name: Checkout Source Code
      uses: actions/checkout@v4

    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: '9.0.x'

    - name: Restore Packages
      run: dotnet restore

    - name: Build Project
      run: dotnet build --no-restore

    - name: Run Tests
      run: dotnet test --no-build
Workflow Overview

This workflow will automatically run when:

Code is pushed to the main branch.
A Pull Request is created or updated targeting the main branch.

The workflow performs the following actions:

Checks out the source code.
Installs the required .NET SDK.
Restores NuGet packages.
Builds the project.
Executes all NUnit test cases.
3.3 Commit and Push the Workflow
git add .
git commit -m "Add GitHub Actions workflow"
git push
3.4 View the Workflow Results

Open your GitHub repository and navigate to:

Repository
→ Actions

You will see the workflow:

Selenium NUnit Tests

Click the workflow run to view detailed execution logs for each step, including:

Checkout Source Code
Setup .NET
Restore Packages
Build Project
Run Tests

If all tests pass successfully, you should see a green check mark indicating that the workflow completed successfully.