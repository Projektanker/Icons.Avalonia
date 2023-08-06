# Changelog

## [8.2.0](https://github.com/Projektanker/Icons.Avalonia/compare/v8.1.0...v8.2.0) (2023-08-06)


### Features

* **FontAwesome:** 🔄 icons changed. ([8e17fbc](https://github.com/Projektanker/Icons.Avalonia/commit/8e17fbc9dcb8bf511ac0d50320558c0f4dc44c2b))

## [8.1.0](https://github.com/Projektanker/Icons.Avalonia/compare/v8.0.0...v8.1.0) (2023-07-26)


### Features

* Sign assemblies with a strong key. ([1101b51](https://github.com/Projektanker/Icons.Avalonia/commit/1101b51262d2ecb5ff0d5387ea98110943e955bd)), closes [#55](https://github.com/Projektanker/Icons.Avalonia/issues/55)


### Bug Fixes

* Fixes the outdated interface documentation ([e695c03](https://github.com/Projektanker/Icons.Avalonia/commit/e695c038b15483b93cef8c9b559214f03c1fa9eb))

## [8.0.0](https://github.com/Projektanker/Icons.Avalonia/compare/v7.0.1...v8.0.0) (2023-07-17)


### ⚠ BREAKING CHANGES

* The `Icon` got an additional `Canvas` encapsulating the `Path`. This may affect styling.

### Bug Fixes

* Do not stretch icon paths to full size. Otherwise icons like [circle-medium](https://pictogrammers.com/library/mdi/icon/circle-medium/) won't be rendered as expected. ([8182200](https://github.com/Projektanker/Icons.Avalonia/commit/81822006afcee08301e93b9464d9055b9ce72f28))

## [7.0.1](https://github.com/Projektanker/Icons.Avalonia/compare/v7.0.0...v7.0.1) (2023-07-16)


### Bug Fixes

* Fixes workflow status badges in README ([8054d9f](https://github.com/Projektanker/Icons.Avalonia/commit/8054d9fb5c2cb8c593331a9e3d0c36ca0e7a48dc))

## [7.0.0](https://github.com/Projektanker/Icons.Avalonia/compare/v6.6.0...v7.0.0) (2023-07-16)


### ⚠ BREAKING CHANGES

* replaces the template with a  `Path` inside  a `Viewbox` instead of an `Image`

### Features

* Adds URL identification format for XML Namespace Definitions ([8128a6d](https://github.com/Projektanker/Icons.Avalonia/commit/8128a6d75f6c29134a625cc611d976eb82e41c99))
* replaces the template with a  `Path` inside  a `Viewbox` instead of an `Image` ([f3792b5](https://github.com/Projektanker/Icons.Avalonia/commit/f3792b52d50db6192d5c8e90fd90b977ae8b9774))


### Bug Fixes

* Calls `AffectsRender` to ensure a redraw if `Value` or `Animation` changed ([6a20461](https://github.com/Projektanker/Icons.Avalonia/commit/6a20461db0289d1267b37dc2f6f2d6017617d926))
