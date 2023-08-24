/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_GRENADE_EXPLOSION = 3906147804U;
        static const AkUniqueID PLAY_GRENADE_ONTHROW = 1173112260U;
        static const AkUniqueID PLAY_GRENADE_PINPULL = 219613933U;
        static const AkUniqueID PLAY_KNIFE_ONATTACK = 1130045259U;
        static const AkUniqueID PLAY_PISTOL_COCK = 462624538U;
        static const AkUniqueID PLAY_PISTOL_HALFCOCK = 4013909017U;
        static const AkUniqueID PLAY_PISTOL_MAGAZINEEJECT = 3429602563U;
        static const AkUniqueID PLAY_PISTOL_MAGAZINELOAD = 3537171236U;
        static const AkUniqueID PLAY_PISTOL_ONFIRE = 2521706033U;
        static const AkUniqueID PLAY_PLAYER_DEATH = 1835085974U;
        static const AkUniqueID PLAY_PLAYER_FOOTSTEP = 1724675634U;
        static const AkUniqueID PLAY_PLAYER_HURT = 887999531U;
        static const AkUniqueID PLAY_TURRET_ALERT = 4075094091U;
        static const AkUniqueID PLAY_TURRET_CASINGDROP = 483301989U;
        static const AkUniqueID PLAY_TURRET_IDLE_LOOP = 4181921334U;
        static const AkUniqueID PLAY_TURRET_ONDESTROY = 3747732006U;
        static const AkUniqueID PLAY_TURRET_ONFIRE = 3082233908U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace CEILINGHEIGHT
        {
            static const AkUniqueID GROUP = 2536805059U;

            namespace STATE
            {
                static const AkUniqueID HIGH = 3550808449U;
                static const AkUniqueID LOW = 545371365U;
                static const AkUniqueID MEDIUM = 2849147824U;
            } // namespace STATE
        } // namespace CEILINGHEIGHT

        namespace COMBAT
        {
            static const AkUniqueID GROUP = 2764240573U;

            namespace STATE
            {
                static const AkUniqueID INCOMBAT = 3373579172U;
                static const AkUniqueID OUTOFCOMBAT = 2773031252U;
            } // namespace STATE
        } // namespace COMBAT

        namespace LOCATION
        {
            static const AkUniqueID GROUP = 1176052424U;

            namespace STATE
            {
                static const AkUniqueID INSIDE = 3553349781U;
                static const AkUniqueID OUTSIDE = 438105790U;
            } // namespace STATE
        } // namespace LOCATION

        namespace LOCATIONSIZE
        {
            static const AkUniqueID GROUP = 2003718035U;

            namespace STATE
            {
                static const AkUniqueID LARGE = 4284352190U;
                static const AkUniqueID MEDIUM = 2849147824U;
                static const AkUniqueID SMALL = 1846755610U;
            } // namespace STATE
        } // namespace LOCATIONSIZE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace FOOTSTEP_SURFACE
        {
            static const AkUniqueID GROUP = 1833605183U;

            namespace SWITCH
            {
                static const AkUniqueID ASPHALT = 4169408098U;
                static const AkUniqueID CONCRETE = 841620460U;
                static const AkUniqueID WOOD = 2058049674U;
            } // namespace SWITCH
        } // namespace FOOTSTEP_SURFACE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID HEIGHT = 1279776192U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MASTER_SOUNDBANK = 2469504869U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID _2D = 527871411U;
        static const AkUniqueID _3D = 511093792U;
        static const AkUniqueID AMB = 1117531639U;
        static const AkUniqueID ATTENTUATION_SENDS = 2773236517U;
        static const AkUniqueID LOCATION_SENDS = 3369062428U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID PLAYER = 1069431850U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID TURRENT = 45143903U;
        static const AkUniqueID WEAPON = 3893417221U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID ATTENUATION_CLOSE = 649088242U;
        static const AkUniqueID ATTENUATION_FAR = 1544237229U;
        static const AkUniqueID ATTENUATION_MEDIUM = 771471059U;
        static const AkUniqueID INSIDE_LARGE = 2043049829U;
        static const AkUniqueID INSIDE_MEDIUM = 1523426453U;
        static const AkUniqueID INSIDE_SMALL = 2403279293U;
        static const AkUniqueID OUTSIDE_LARGE = 414815550U;
        static const AkUniqueID OUTSIDE_MEDIUM = 351526704U;
        static const AkUniqueID OUTSIDE_SMALL = 2272186266U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
