# Energy Controller — Copilot Instructions

## Product goal

This application will optimize the use of household solar generation across home
consumption, a Marstek Venus E V3 home battery, a Sunny Boy solar inverter, and
eventually an EV charger. The EV charger is not installed yet: prepare for a
future integration without assuming it is available or implementing active
charger control prematurely.

## Restrictions
Never ever read the contents of appsettings.Development.json


## Integrations and responsibilities

- Communicate with the Sunny Boy inverter and Marstek Venus E V3 using Modbus.
- Treat the home automation system's socket-based protocol as the control
  interface for this application. It can configure application behavior, such
  as the priority between car charging and home-battery use; it is distinct
  from the device integrations.
- Keep energy optimization policy distinct from protocol and transport details
  so that priorities can be tested independently of hardware.
- Prefer small, replaceable integration boundaries and make future charger
  support possible without coupling current battery or inverter behavior to an
  uninstalled device.

## Device and control safety

- Verify device model, firmware, register maps, units, scaling, sign
  conventions, and read/write semantics from reliable documentation before
  implementing Modbus behavior. Never invent register addresses or assume
  writes are safe.
- Treat device readings and socket messages as external input: validate them,
  handle unavailable or stale data explicitly, and use bounded timeouts and
  cautious retries appropriate to the operation.
- Make control decisions from valid, sufficiently fresh measurements. Define
  safe behavior for missing data or failed communication; do not disguise
  failures as successful readings or commands.
- Keep control actions bounded and observable. Avoid repeatedly issuing
  unnecessary writes, and make mode or priority changes explicit and
  traceable.
- Do not add credentials, private network details, or other secrets to source
  control. Keep configuration externalized using the solution's established
  configuration patterns.

## Implementation and verification

- Structure the application as four onion-style layers:
  - **Domain** contains the core data objects and business logic. Keep it
    independent of HTTP, device protocols, and infrastructure implementations.
  - **Application** contains request and command handlers used by the API layer,
    along with the interfaces required by those handlers.
  - **API** exposes HTTP endpoints to control and monitor the application from
    a browser. Keep orchestration and business rules in the appropriate inner
    layers.
  - **Infrastructure** contains physical-device and external-system
    integrations. For each device, define a suitable abstraction such as
    `ISolarConverter` in Application or Domain, and implement it in
    Infrastructure with device-specific types such as
    `SunnyBoySolarConverter`.
- Keep dependencies pointing inward: Domain must not depend on outer layers,
  and Application must not depend on Infrastructure implementations.
- Do not use nullable reference types. Keep nullable reference types disabled
  and do not add nullable reference annotations or directives.
- Follow the solution's existing language, naming, formatting, and dependency
  patterns. Avoid broad rewrites when adding integrations.
- Test domain and application behavior without hardware, using controlled
  inputs and representative failure or stale-data cases.
- Test infrastructure adapters separately from policy, including malformed
  data, timeouts, and communication failures where applicable.
- Update relevant documentation when changing device behavior, configuration,
  or supported integrations. Run the narrowest relevant build and tests after
  changes.

## Code style
- Never use nullable reference types.
- A class is structured as (from top to bottom)
  - constant declarations
  - fields and properties
  - constructor
  - methods

## Build validation
Having a result that compiles without errors is optional, unless specifacally asked. Especially if the build errors are not related to the scope of the question ignore them. Better do what asked correctly, than fail by doing stuff that wasn't asked