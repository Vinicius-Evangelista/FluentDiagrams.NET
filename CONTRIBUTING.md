<!-- omit in toc -->
# Contributing to FluentDiagrams.NET

First off, thanks for taking the time to contribute! ❤️

All types of contributions are encouraged and valued. See the [Table of Contents](#table-of-contents) for different ways to help and details about how this project handles them. Please make sure to read the relevant section before making your contribution. It will make it a lot easier for us maintainers and smooth out the experience for all involved. The community looks forward to your contributions. 🎉

> And if you like the project, but just don't have time to contribute, that's fine. There are other easy ways to support the project and show your appreciation, which we would also be very happy about:
> - Star the project
> - Tweet about it
> - Refer this project in your project's readme
> - Mention the project at local meetups and tell your friends/colleagues

<!-- omit in toc -->
## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [I Have a Question](#i-have-a-question)
  - [I Want To Contribute](#i-want-to-contribute)
  - [Reporting Bugs](#reporting-bugs)
  - [Suggesting Enhancements](#suggesting-enhancements)
  - [Your First Code Contribution](#your-first-code-contribution)
  - [Improving The Documentation](#improving-the-documentation)
- [Styleguides](#styleguides)
  - [Commit Messages](#commit-messages)
- [Join The Project Team](#join-the-project-team)


## Code of Conduct

This project and everyone participating in it is governed by the
[FluentDiagrams.NEt Code of Conduct](https://github.com/Vinicius-Evangelista/FluentDiagrams.NET/blob/main/CODE_OF_CONDUCT.md).
By participating, you are expected to uphold this code. Please report unacceptable behavior
to <viniciusevangelista448@gmail.com>.


## I Have a Question

> If you want to ask a question, we assume that you have read the available [Documentation]().

Before you ask a question, it is best to search for existing [Issues](https://github.com/Vinicius-Evangelista/FluentDiagrams.NET/issues) that might help you. In case you have found a suitable issue and still need clarification, you can write your question in this issue. It is also advisable to search the internet for answers first.

If you then still feel the need to ask a question and need clarification, we recommend the following:

- Open an [Issue](https://github.com/Vinicius-Evangelista/FluentDiagrams.NET/issues/new).
- Provide as much context as you can about what you're running into.
- Provide project and platform versions (nodejs, npm, etc), depending on what seems relevant.

We will then take care of the issue as soon as possible.


## I Want To Contribute

> ### Legal Notice <!-- omit in toc -->
> When contributing to this project, you must agree that you have authored 100% of the content, that you have the necessary rights to the content and that the content you contribute may be provided under the project licence.

### Reporting Bugs

<!-- omit in toc -->
#### Before Submitting a Bug Report

A good bug report shouldn't leave others needing to chase you up for more information. Therefore, we ask you to investigate carefully, collect information and describe the issue in detail in your report. Please complete the following steps in advance to help us fix any potential bug as fast as possible.

- Make sure that you are using the latest version.
- Determine if your bug is really a bug and not an error on your side e.g. using incompatible environment components/versions (Make sure that you have read the [documentation](). If you are looking for support, you might want to check [this section](#i-have-a-question)).
- To see if other users have experienced (and potentially already solved) the same issue you are having, check if there is not already a bug report existing for your bug or error in the [bug tracker](https://github.com/Vinicius-Evangelista/FluentDiagrams.NET/issues?q=label%3Abug).
- Also make sure to search the internet (including Stack Overflow) to see if users outside of the GitHub community have discussed the issue.
- Collect information about the bug:
  - Stack trace (Traceback)
  - OS, Platform and Version (Windows, Linux, macOS, x86, ARM)
  - Version of the interpreter, compiler, SDK, runtime environment, package manager, depending on what seems relevant.
  - Possibly your input and the output
  - Can you reliably reproduce the issue? And can you also reproduce it with older versions?

<!-- omit in toc -->
#### How Do I Submit a Good Bug Report?

> You must never report security related issues, vulnerabilities or bugs including sensitive information to the issue tracker, or elsewhere in public. Instead sensitive bugs must be sent by email to <viniciusevangelista448@gmail.com>.
<!-- You may add a PGP key to allow the messages to be sent encrypted as well. -->

We use GitHub issues to track bugs and errors. If you run into an issue with the project:

- Open an [Issue](https://github.com/Vinicius-Evangelista/FluentDiagrams.NET/issues/new). (Since we can't be sure at this point whether it is a bug or not, we ask you not to talk about a bug yet and not to label the issue.)
- Explain the behavior you would expect and the actual behavior.
- Please provide as much context as possible and describe the *reproduction steps* that someone else can follow to recreate the issue on their own. This usually includes your code. For good bug reports you should isolate the problem and create a reduced test case.
- Provide the information you collected in the previous section.

Once it's filed:

- The project team will label the issue accordingly.
- A team member will try to reproduce the issue with your provided steps. If there are no reproduction steps or no obvious way to reproduce the issue, the team will ask you for those steps and mark the issue as `needs-repro`. Bugs with the `needs-repro` tag will not be addressed until they are reproduced.
- If the team is able to reproduce the issue, it will be marked `needs-fix`, as well as possibly other tags (such as `critical`), and the issue will be left to be [implemented by someone](#your-first-code-contribution).

<!-- You might want to create an issue template for bugs and errors that can be used as a guide and that defines the structure of the information to be included. If you do so, reference it here in the description. -->


### Suggesting Enhancements

This section guides you through submitting an enhancement suggestion for FluentDiagrams.NEt, **including completely new features and minor improvements to existing functionality**. Following these guidelines will help maintainers and the community to understand your suggestion and find related suggestions.

<!-- omit in toc -->
#### Before Submitting an Enhancement

- Make sure that you are using the latest version.
- Read the [documentation]() carefully and find out if the functionality is already covered, maybe by an individual configuration.
- Perform a [search](https://github.com/Vinicius-Evangelista/FluentDiagrams.NET/issues) to see if the enhancement has already been suggested. If it has, add a comment to the existing issue instead of opening a new one.
- Find out whether your idea fits with the scope and aims of the project. It's up to you to make a strong case to convince the project's developers of the merits of this feature. Keep in mind that we want features that will be useful to the majority of our users and not just a small subset. If you're just targeting a minority of users, consider writing an add-on/plugin library.

<!-- omit in toc -->
#### How Do I Submit a Good Enhancement Suggestion?

Enhancement suggestions are tracked as [GitHub issues](https://github.com/Vinicius-Evangelista/FluentDiagrams.NET/issues).

- Use a **clear and descriptive title** for the issue to identify the suggestion.
- Provide a **step-by-step description of the suggested enhancement** in as many details as possible.
- **Describe the current behavior** and **explain which behavior you expected to see instead** and why. At this point you can also tell which alternatives do not work for you.
- You may want to **include screenshots or screen recordings** which help you demonstrate the steps or point out the part which the suggestion is related to. You can use [LICEcap](https://www.cockos.com/licecap/) to record GIFs on macOS and Windows, and the built-in [screen recorder in GNOME](https://help.gnome.org/users/gnome-help/stable/screen-shot-record.html.en) or [SimpleScreenRecorder](https://github.com/MaartenBaert/ssr) on Linux. 
- **Explain why this enhancement would be useful** to most FluentDiagrams.NEt users. You may also want to point out the other projects that solved it better and which could serve as inspiration.

#### Issue Template
```markdown
--
name: 🐞 Bug Report
about: Report a bug to help us improve FluentDiagrams.NET
title: "[Bug] "
labels: bug
assignees: ''
---

## Description

A clear and concise description of the bug.

## Steps to Reproduce

1. Go to '...'
2. Click on '...'
3. Scroll down to '...'
4. See error

## Expected Behavior

A clear and concise description of what you expected to happen.

## Screenshots

If applicable, add screenshots to help explain your problem.

## Environment

- FluentDiagrams.NET version: 
- .NET SDK version: 
- OS: 

## Additional Context

Add any other context about the problem here.
```

### Your First Code Contribution

---

####  First Contribution Guide

Welcome to the FluentDiagrams.NET project! We're excited to have you here. This guide will walk you through setting up your development environment and making your first contribution.

####  Prerequisites

Before you begin, ensure you have the following tools installed:

* **.NET SDK**: [Download the latest .NET SDK](https://dotnet.microsoft.com/download)
* **Git**: [Download Git](https://git-scm.com/downloads)
* **IDE or Editor**: We recommend [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) or 

####  Setting Up the Development Environment

1. **Fork the Repository**

   Click the "Fork" button at the top right of the [FluentDiagrams.NET repository](https://github.com/yourusername/FluentDiagrams.NET) to create your own copy.

2. **Clone Your Fork**

   Open a terminal and run:

   ```bash
   git clone https://github.com/yourusername/FluentDiagrams.NET.git
   cd FluentDiagrams.NET
   ```



3. **Create a New Branch**

   It's good practice to create a new branch for your work:

   ```bash
   git checkout -b your-feature-name
   ```



4. **Restore Dependencies**

   Use the .NET CLI to restore project dependencies:

   ```bash
   dotnet restore
   ```



5. **Build the Project**

   Build the project to ensure everything is set up correctly:

   ```bash
   dotnet build
   ```



6. **Run Tests**

   Run the existing tests to verify that the project is working as expected:

   ```bash
   dotnet test
   ```

####  Making Your First Contribution

1. **Make Your Changes**

   Implement your changes or additions in the appropriate files.

2. **Commit Your Changes**

   After making changes, commit them with a descriptive message:

   ```bash
   git add .
   git commit -m "Description of your changes"
   ```



3. **Push to Your Fork**

   Push your changes to your forked repository:

   ```bash
   git push origin your-feature-name
   ```



4. **Create a Pull Request**

   Go to your forked repository on GitHub and click the "Compare & pull request" button. Provide a clear and concise description of your changes.

###  Additional Resources

* **.NET Documentation**: [https://docs.microsoft.com/en-us/dotnet/](https://docs.microsoft.com/en-us/dotnet/)
* **GitHub Docs**: [https://docs.github.com/](https://docs.github.com/)
* **Visual Studio Code Docs**: [https://code.visualstudio.com/docs](https://code.visualstudio.com/docs)

---

Feel free to reach out if you have any questions or need assistance. Happy coding!



#### Pull request template:
```markdown
#  Title
Is a simple title :)

##  Description

Please provide a concise summary of the changes introduced in this PR. Highlight the purpose and any relevant context.

##  Related Issues

Reference any related issues by mentioning them (e.g., `Closes #123`, `Fixes #456`).

##  Testing

Describe the testing performed to validate these changes. Include details about test cases, environments, or any other relevant information.

##  Checklist

- [ ] The code adheres to the project's coding standards.
- [ ] Unit tests have been added or updated to cover the changes.
- [ ] Documentation has been updated accordingly.
- [ ] All existing and new tests pass successfully.
- [ ] The PR title follows the [Conventional Commits](https://www.conventionalcommits.org/) guidelines.

##  Screenshots (if applicable)

Include any relevant screenshots or visual aids to illustrate the changes.

##  Additional Notes

Provide any additional information or context that might be helpful for reviewers.

```

### Improving The Documentation (Template)
* Sent as an issue
```markdown
---
name:  Documentation Improvement
about: Suggest enhancements or corrections to the FluentDiagrams.NET documentation.
title: "[Docs] "
labels: documentation
assignees: ''
---

##  Section of Documentation

Please specify the section or page of the documentation that requires improvement. Provide a link if possible.

##  Description of the Improvement

Clearly describe the issue or enhancement you are proposing. Be as specific as possible.

##  Suggested Changes

If you have specific suggestions or edits, please detail them here. You may include code snippets, examples, or rewritten text.

##  Rationale

Explain why this change would improve the documentation. Consider aspects like clarity, accuracy, completeness, or user experience.

##  Additional Resources

If applicable, include links to relevant resources, discussions, or examples that support your suggestion.

##  Additional Context

Add any other context or information that might be helpful in understanding the issue.
```
## Branch
We have two type of branchs, feat/* and fix/* and that's enought for now.

## Styleguides
The style guide is enforced via .editorconfig :)

<!-- omit in toc -->
## Attribution
This guide is based on the [contributing.md](https://contributing.md/generator)!
